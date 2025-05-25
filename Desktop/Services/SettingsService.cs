using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Desktop.Interfaces;
using Desktop.Views;

namespace Desktop.Services
{
    public class SettingsService: ISettingsService
    {
        private readonly SettingsView _view;

        public SettingsService(SettingsView view)
        {
            _view = view;
        }
        public void ShowUI(Window? owener = null)
        {
            _view.Owner = owener;
            _view.ShowDialog();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Store()
        {
            throw new NotImplementedException();
        }
    }
}
