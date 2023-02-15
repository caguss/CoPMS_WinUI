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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Vanara;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.popup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReserveCarPopup : ContentDialog
    {
        string carcode;
        string toGo;
        DateTime reservedate;
        string carname;
        
        public ReserveCarPopup(string carCode, string togo, DateTime date, string carKindName)
        {
            this.InitializeComponent();

            string msg = string.Format("차량 종류 : {0}\r\n목적지 : {1}\r\n날짜 : {2}", carKindName, togo, date.ToString("yyyy-MM-dd"));
            txtMsg.Text= msg;
            carcode = carCode;
            toGo = togo;
            reservedate = date;
            carname = carKindName;
            txtUser.Text = "이름 : " + App.loginUser.UserName;
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

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Provider.CarReservation(reservedate, toGo, carcode);
                this.Hide();
                string okmsg = "차량이 예약되었습니다.";
                PopupMessage(okmsg);
            }

            catch (Exception ex)
            {
                PopupMessage(ex.Message);
            }
        }
    }
}
