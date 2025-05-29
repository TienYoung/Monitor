using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Interfaces
{
    public interface IProgressService
    {
        public event Action? Starting;
        
        public event Action<bool>? Stopped;

        public RoutedEventHandler DialogLoaded { set; }

        public IProgress<int> Progress { get; }

        public CancellationToken Token { get; }
    }
}
