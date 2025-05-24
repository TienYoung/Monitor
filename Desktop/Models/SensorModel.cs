using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Serilog;

namespace Desktop.Models
{
    public partial class SensorModel : ObservableObject
    {
        [ObservableProperty]
        private double? _currentValue;

        public void AddDataRecord(DateTime timestamp, double value)
        {
            var logger = Ioc.Default.GetRequiredService<ILogger>();
            logger.Information($"{value}");
        }
    }
}
