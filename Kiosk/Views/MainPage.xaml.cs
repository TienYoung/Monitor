using CommunityToolkit.Mvvm.DependencyInjection;
using Kiosk.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Kiosk.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CoreApplicationViewTitleBar _coreTitleBar;
        private ApplicationViewTitleBar _titleBar;

        public MainPage()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<MainViewModel>();

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(AppTitleBar);
            // Register a handler for when the window activation changes.
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;

            // Hide default title bar.
            _coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            _coreTitleBar.ExtendViewIntoTitleBar = true;

            // Register a handler for when the size of the overlaid caption control changes.
            _coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            _coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            // Set caption buttons background to transparent.
            _titleBar = ApplicationView.GetForCurrentView().TitleBar;
            _titleBar.ButtonBackgroundColor = Colors.Transparent;
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            // Get the size of the caption controls and set padding.
            LeftPaddingColumn.Width = new GridLength(_coreTitleBar.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(_coreTitleBar.SystemOverlayRightInset);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args)
        {
            UISettings settings = new UISettings();
            if (args.WindowActivationState == CoreWindowActivationState.Deactivated)
            {
                AppTitleTextBlock.Foreground = new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
            }
            else
            {
                AppTitleTextBlock.Foreground = new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
            }
        }
    }
}
