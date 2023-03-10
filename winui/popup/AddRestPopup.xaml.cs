// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.popup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRestPopup : ContentDialog
    {

        string restkind1;
        string reason1;
        DateTime date1;
        string kindName;
        public AddRestPopup(string restKind, string reason, DateTime date, string restKindName)
        {
            this.InitializeComponent();
            string msg = string.Format( "연차 종류 : {0}\r\n연차 사유 : {1}\r\n날짜 : {2}", restKindName, reason, date.ToString("yyyy-MM-dd"));
         
            txtMsg.Text = msg;
            restkind1 = restKind;
            reason1 = reason;
            date1 = date;
            kindName = restKindName;
            txtUser.Text ="이름 : "+ App.loginUser.UserName;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();

            dt = Provider.RestRegister(restkind1, date1, reason1);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.Hide();
                    string okmsg = "연차 등록이 완료되었습니다";
                    PopupMessage(okmsg);
                }
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }
    }
}
