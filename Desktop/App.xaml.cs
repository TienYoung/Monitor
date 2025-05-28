using CommunityToolkit.Mvvm.DependencyInjection;
using Desktop.Interfaces;
using Desktop.Enums;
using Desktop.Models;
using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ISensorService<SensorModel>, MockSensor>()
                .AddSingleton<ILogger>(
                    new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("sensor.log")
                    .CreateLogger())
                .AddSingleton<SettingsService>()
                .AddSingleton<ISettingsService>(sp => sp.GetRequiredService<SettingsService>())
                .AddKeyedSingleton<ISubWindow>(SubWindowTypes.Settings, (sp, _) => sp.GetRequiredService<SettingsService>())
                .AddScoped<ProgressService>()
                .AddScoped<IProgressService>(sp => sp.GetRequiredService<ProgressService>())
                .AddKeyedScoped<ISubWindow>(SubWindowTypes.Progress, (sp, _) => sp.GetRequiredService<ProgressService>())
                .AddTransient<MainWindowViewModel>()
                .AddTransient<MainWindowView>(sp => new() { DataContext = sp.GetRequiredService<MainWindowViewModel>() })
                .AddTransient<SettingsViewModel>()
                .AddTransient<SettingsView>(sp => new() { DataContext = sp.GetRequiredService<SettingsViewModel>() })
                .BuildServiceProvider());
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Ioc.Default.GetRequiredService<ISensorService<SensorModel>>().Start();
            Ioc.Default.GetRequiredService<MainWindowView>().Show();
            Ioc.Default.GetRequiredService<ISettingsService>().Load();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Ioc.Default.GetRequiredService<ISensorService<SensorModel>>().Stop();
        }
    }

}
