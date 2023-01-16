// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;
using Microsoft.UI;
using winui.Helper;
using WinUIGallery.DesktopWap.Helper;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
            Loaded += OnSettingsPageLoaded;
        }

        private void RbLight_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["#ColorTheme"] = "Light";
        }

        private void RbDark_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["#ColorTheme"] = "Dark";
        }

        private void RbSystem_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["#ColorTheme"] = "System";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = (this.RequestedTheme == ElementTheme.Dark) ? ElementTheme.Light : ElementTheme.Dark;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Login login= new Login();
            login.Activate();
        }

        private void OnThemeRadioButtonKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Up)
            {
              //  NavigationRootPage.GetForElement(this).PageHeader.Focus(FocusState.Programmatic);
            }
        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            var res = Microsoft.UI.Xaml.Application.Current.Resources;
            Action<Windows.UI.Color> SetTitleBarButtonForegroundColor = (Windows.UI.Color color) => { res["WindowCaptionForeground"] = color; };

            if (selectedTheme != null)
            {
                ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
                if (selectedTheme == "Dark")
                {
                    SetTitleBarButtonForegroundColor(Colors.White);
                }
                else if (selectedTheme == "Light")
                {
                    SetTitleBarButtonForegroundColor(Colors.Black);
                }
                else
                {
                    if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    {
                        SetTitleBarButtonForegroundColor(Colors.White);
                    }
                    else
                    {
                        SetTitleBarButtonForegroundColor(Colors.Black);
                    }
                }
            }
            var window = WindowHelper.GetWindowForElement(this);
            TitleBarHelper.triggerTitleBarRepaint(window);

        }

        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            var currentTheme = ThemeHelper.RootTheme.ToString();
            (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
           
        }

        private void soundPageHyperlink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {

        }

        private void navigationLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FolderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void screenshotFolderLink_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnResetTeachingTipsButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void screenshotModeToggle_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void soundToggle_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void spatialSoundBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void spatialSoundBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
