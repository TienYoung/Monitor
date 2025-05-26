using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFLocalizeExtension.Engine;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Desktop.ViewModels
{
    public enum Language
    {
        English,
        Chinese,
    }

    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Language _currentLanguge = Language.English;

        [ObservableProperty]
        private double _refreshRate = 2.0;

        [RelayCommand]
        private void Save()
        {
            Apply();
            Close();
        }

        [RelayCommand]
        private void Apply()
        {
            var culture = CurrentLanguge switch
            {
                Language.English => ChangeLanguage("en-US"),
                Language.Chinese => ChangeLanguage("zh-CN"),
                _ => ChangeLanguage("en-US"),
            };

            WeakReferenceMessenger.Default.Send(new PropertyChangedMessage<double>(this, nameof(RefreshRate), 2.0, RefreshRate));
        }

        [RelayCommand]
        private void Close()
        {
            var message = WeakReferenceMessenger.Default.Send(new RequestMessage<bool>());
            message.Reply(false);
        }

        private CultureInfo ChangeLanguage(string local)
        {
            return LocalizeDictionary.Instance.Culture
                = Thread.CurrentThread.CurrentCulture
                = Thread.CurrentThread.CurrentUICulture
                = new CultureInfo(local);
        }
    }
}
