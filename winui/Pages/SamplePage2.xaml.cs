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
using winui.popup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplePage2 : Page
    {
        public SamplePage2()
        {
            this.InitializeComponent();
        }


        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            calview2.SetDisplayDate(DateTimeOffset.Now);
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            if (cbCar.SelectedItem == null || txtloc.Text.Length < 1)
            {
                string message = "차량을 선택해주세요.";
                PopupMessage(message);
            }
        }

        private void btnStDrive_Click(object sender, RoutedEventArgs e)
        {
            if(cbCarName.SelectedItem==null)
            {
                string msg = "차량을 선택해주세요.";
                PopupMessage(msg);
            }
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
