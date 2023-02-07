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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.popup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddWorkPopup : ContentDialog
    {
        GoToWorkViewModel gvm;
        DateTime selectdate = DateTime.Now;
        public AddWorkPopup()
        {
            this.InitializeComponent();
            SetText();
        }

        private void SetText()
        {
            txtUser.Text = App.loginUser.UserName;
            gvm = new GoToWorkViewModel(selectdate);
            if (gvm.IsStartWork)
                btnStartWork.Content = "출근";
            else
                btnStartWork.Content = "퇴근";
        }

        private void btnStartWork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Provider.WorkInOut(Convert.ToInt32(App.loginUser.UserID), gvm.IsStartWork);
                this.Hide();
                PopupMessage("퇴근하셨습니다.");
            }

            catch
            {
            }
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
