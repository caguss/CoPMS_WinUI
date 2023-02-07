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
using System.Runtime.InteropServices;

using winui.Properties;
using winui.Helper;
using AppUIBasics;

using System.Data;
using winui.popup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : ContentDialog
    {
        //   private static Window startupWindow;

        MainWindow main;
        public Login()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void chkLogin_Click(object sender, RoutedEventArgs e)
        {
            //로그인 상태 유지 
            if(chkLogin.IsChecked == true)
            {
                
            }
            else
            {

            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginCheck();
        }

        private void LoginCheck()
        {
            string id = txtID.Text;
            //string pw = txtPW.TextReadingOrder.ToString();
            string pw = txtPW.Password.ToString();
            string platform = "winui";
            DataTable dt = Provider.Login(id, pw, platform);           
            if(dt.Rows.Count > 0)
            {
                App.loginUser.UserName = dt.Rows[0]["사용자이름"].ToString();
                App.loginUser.UserID = dt.Rows[0]["사용자코드"].ToString();
                
                this.Hide();
            }

            else 
            {
                this.Hide();
                string message = "로그인 정보가 일치 하지않습니다.";
                PopupMessage(message);
            }
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }

        private void txtPW_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
                btnLogin_Click(sender,e);
        }

     
        private void StackPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
               e.Handled= true; 
            }
        }
    }
}
