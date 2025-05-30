using Desktop.Interfaces;
using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Services
{
    public class ProgressService : IProgressService, ISubWindow
    {
        public event Action? Starting;

        public event Action<bool>? Stopped;

        private ProgressDialogView _dialog { get; } = new();

        public RoutedEventHandler DialogLoaded { set => _dialog.Loaded += value; }

        public IProgress<int> Progress => _dialog.Progress;

        public CancellationToken Token => _dialog.Token;

        public void ShowUI(Window? owner = null)
        {
            OnProgressStarting();

            _dialog.Owner = owner;

            OnProgressStopped(_dialog.ShowDialog() ?? false);
        }

        private void OnProgressStarting()
        {
            Starting?.Invoke();
        }

        private void OnProgressStopped(bool result)
        {
            Stopped?.Invoke(result);
        }
    }
}
