using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Desktop.Interfaces;
using Desktop.Views;
using Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop.Services
{
    public class SettingsService: ISettingsService, ISubWindow
    {
        public void ShowUI(Window? owener = null)
        {
            var view = Ioc.Default.GetRequiredService<SettingsView>();
            view.Owner = owener;
            view.ShowDialog();
        }

        public void Load()
        {
            Ioc.Default.GetRequiredService<SettingsViewModel>().ApplyCommand.Execute(null);
        }

        public void Store()
        {
            throw new NotImplementedException();
        }
    }
}
