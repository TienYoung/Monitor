using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Desktop.Interfaces;
using Desktop.Models;
using Desktop.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Desktop.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _text = "设置如上后，所有以下内容都将使用 Noto 字体";

        [ObservableProperty]
        private double? _data;

        [RelayCommand]
        private void OpenSettings(object sender)
        {
            if (sender is Window owner)
            {
                var progressService = Ioc.Default.GetRequiredService<IProgressService>();
                progressService.Starting += (progress, token) =>
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            token.ThrowIfCancellationRequested();
                            for (int i = 1; i <= 100; i++)
                            {
                                progress.Report(i);
                                await Task.Delay(50, token).ConfigureAwait(false);
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            // Handle cancellation if needed
                        }
                    }, token);
                };

                progressService.Stopped += (result) =>
                {
                    if (result == false)
                    {
                        return;
                    }
                    var sp = Ioc.Default.GetRequiredService<IServiceProvider>();
                    sp.GetRequiredKeyedService<ISubWindow>(SubWindowTypes.Settings).ShowUI(owner);
                };
                var sp = Ioc.Default.GetRequiredService<IServiceProvider>();
                sp.GetRequiredKeyedService<ISubWindow>(SubWindowTypes.Progress).ShowUI(owner);
            }
        }

        public MainWindowViewModel(ISensorService<SensorModel> sernsorService)
        {
            double refreshRate = 0.0;
            WeakReferenceMessenger.Default.Register<PropertyChangedMessage<double>>(this, (_, m) =>
            {
                if (m.PropertyName == "RefreshRate")
                {
                    refreshRate = m.NewValue;
                }
            });

            var lastUpdate = DateTime.MinValue;

            var model = sernsorService.DataModel;
            model.PropertyChanged += (s, e) =>
            {
                double interval = 1000 / refreshRate;
                
                if(Double.IsInfinity(interval))
                {
                    Data = null;
                    return;
                }

                if (DateTime.Now - lastUpdate > TimeSpan.FromMilliseconds(interval))
                {
                    lastUpdate = DateTime.Now;

                    if (e.PropertyName == nameof(model.CurrentValue))
                    {
                        Data = model.CurrentValue;
                    }
                }
            };
        }
    }
}
