using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Interfaces
{
    public interface ISettingsService
    {
        public void Load();
        public void Store();

        public void ShowUI(Window? owener = null);
    }
}
