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
using Windows.Media.ClosedCaptioning;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.popup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagePopup : ContentDialog
    {
        public MessagePopup(string msg)
        {
            this.InitializeComponent();

            txtDetail.Text = msg;

            this.CloseButtonText = "OK";
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
            if (txtDetail.Text.Contains("로그인"))
            {
                Login();
            }
        }
        private async void Login()
        {

            Login login = new Login();

            login.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;

            login.XamlRoot = this.XamlRoot;

            await login.ShowAsync();

        }
    }
}
