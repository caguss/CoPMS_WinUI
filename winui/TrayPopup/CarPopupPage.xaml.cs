// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using AppUIBasics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using winui.popup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.TrayPopup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CarPopupPage : Window
    {
        CarViewModel carlist = new CarViewModel();

        public CarPopupPage()
        {
            this.InitializeComponent();

            Win32.RegisterWindowMinMax(this, 500, 500, 300, 300);
            Window window = this;


            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            if (appWindow is not null)
            {
                Microsoft.UI.Windowing.DisplayArea displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                if (displayArea is not null)
                {
                    var CenteredPosition = appWindow.Position;
                    CenteredPosition.X = displayArea.WorkArea.Width - 500;
                    CenteredPosition.Y = displayArea.WorkArea.Height - 300;
                    appWindow.Move(CenteredPosition);
                }
            }

            window.ExtendsContentIntoTitleBar = true;
            window.SetTitleBar(pnlTitle);
            window.Activate();

            datepic.SelectedDate = new DateTimeOffset(DateTime.Today);
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            if (txttogo.Text.Length < 1)
            {
                string msg = "목적지를 입력해주세요.";
                PopupMessage(msg);
            }
            else
            {
                DateTime dateTime = datepic.Date.DateTime;
                string carKindName = carlist.PickerChoices[cbCar.SelectedIndex].CarName.ToString();
                ReserveCarPopup(cbCar.SelectedValue.ToString(), txttogo.Text, dateTime, carKindName);
            }
        }


        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.Content.XamlRoot;

            await msg.ShowAsync();
        }

        public async void ReserveCarPopup(string carCode, string togo, DateTime date, string carKindName)
        {
            ReserveCarPopup arp = new ReserveCarPopup(carCode, togo, date, carKindName);

            arp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            arp.XamlRoot = this.Content.XamlRoot;

            await arp.ShowAsync();
        }
    }
}
