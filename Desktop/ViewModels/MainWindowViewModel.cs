using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Desktop.Interfaces;
using Desktop.Models;
using System.Globalization;
using System.Reflection.Metadata;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace Desktop.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private String _text = "设置如上后，所有以下内容都将使用 Noto 字体";

        [ObservableProperty]
        private Double? _data;

        [RelayCommand]
        private void OpenSettings(object sender)
        {
            if (sender is Window owner)
            {
                Ioc.Default.GetRequiredService<ISettingsService>().ShowUI(owner);
            }
        }

        [ObservableProperty]
        private double _refreshRate = 2;

        public MainWindowViewModel(ISensorService<SensorModel> sernsorService)
        {
            var lastUpdate = DateTime.MinValue;

            var model = sernsorService.DataModel;
            model.PropertyChanged += (s, e) =>
            {
                if (DateTime.Now - lastUpdate > TimeSpan.FromMilliseconds(1000 / _refreshRate))
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
