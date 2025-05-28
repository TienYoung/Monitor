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
        public event Action<IProgress<int>, CancellationToken>? Starting;

        public event Action<bool>? Stopped;

        private CancellationTokenSource? _cts;

        public void ShowUI(Window? owener = null)
        {
            var progressDialog = new ProgressDialogView();
            progressDialog.Owner = owener;
            _cts = new CancellationTokenSource();

            OnProgressStarting(progressDialog.Percentage, _cts.Token);
            OnProgressStopped(progressDialog.ShowDialog() ?? false);
        }

        private void OnProgressStarting(IProgress<int> progress, CancellationToken token)
        {
            Starting?.Invoke(progress, token);
        }

        private void OnProgressStopped(bool result)
        {
            _cts?.Cancel();
            Stopped?.Invoke(result);
        }
    }
}
