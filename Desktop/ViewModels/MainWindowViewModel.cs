using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Desktop.Interfaces;
using Desktop.Models;
using System.Globalization;
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
        private void ChangeLanguage(string local)
        {
            LocalizeDictionary.Instance.Culture 
                = Thread.CurrentThread.CurrentCulture 
                = Thread.CurrentThread.CurrentUICulture 
                = new CultureInfo(local);
        }

        public MainWindowViewModel()
        {
            var lastUpdate = DateTime.MinValue;

            var model = Ioc.Default.GetRequiredService<ISensorService<SensorModel>>().DataModel;
            model.PropertyChanged += (s, e) =>
            {
                if (DateTime.Now - lastUpdate > TimeSpan.FromMilliseconds(500))
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
