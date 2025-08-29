using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Kiosk.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial int Price { get; set; }

        public MainViewModel()
        {
            Price = 14;
        }
    }
}
