using Kiosk.Controls;
using Microsoft.UI.Windowing;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kiosk
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppWindow.SetPresenter(FullScreenPresenter.Create());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var progressDialog = new ProgressDialog()
            {
                XamlRoot = Root.XamlRoot,
                Title = "Running"
            };

            progressDialog.Opened += async (_, _) =>
            {
                var token = progressDialog.Token;
                try
                {
                    token.ThrowIfCancellationRequested();
                    for (int i = 1; i <= 100; i++)
                    {
                        progressDialog.Percentage.Report(i);
                        await Task.Delay(100, token);
                    }
                }
                catch (OperationCanceledException)
                {
                }
            };

            await progressDialog.ShowAsync();
        }
    }
}
