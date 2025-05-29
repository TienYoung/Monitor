using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop.Views
{
    /// <summary>
    /// Interaction logic for ProgressDialogView.xaml
    /// </summary>
    public partial class ProgressDialogView : Window
    {
        public IProgress<int> Progress { get; }
        
        public CancellationToken Token => _cts.Token;

        private CancellationTokenSource _cts { get; }

        public ProgressDialogView()
        {
            InitializeComponent();

            Progress = new Progress<int>((value) => 
            {
                if (value == 100) DialogResult = true;

                PercentProgressBar.Value = value;
            });

            _cts = new CancellationTokenSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
            DialogResult = false;
        }
    }
}
