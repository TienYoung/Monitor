using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kiosk.Controls
{
    public sealed partial class ProgressDialog : ContentDialog
    {
        public IProgress<double> Percentage { get; }

        public CancellationToken Token => _cts.Token;

        private CancellationTokenSource _cts;

        public ProgressDialog()
        {
            InitializeComponent();

            _cts = new CancellationTokenSource();

            Percentage = new Progress<double>(value => 
            {
                ProgressBar.Value = value;
            });
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            _cts.Cancel();
        }
    }
}
