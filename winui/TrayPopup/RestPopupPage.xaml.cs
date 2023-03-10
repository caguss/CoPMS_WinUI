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
using AppUIBasics;
using Microsoft.UI.Xaml.Media.Animation;
using static Vanara.PInvoke.User32;
using System.Data;
using winui.popup;
using Vanara.Extensions.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.TrayPopup
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class RestPopupPage : Window
    {
        RestViewModel restlistpop = new RestViewModel();

        public RestPopupPage()
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

        private void cbSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtReason.Text.Length < 1)
            {
                string msg = "?????? ????????? ??????????????????";
                PopupMessage(msg);
            }

            else
            {
                DateTime dateTime = datepic.Date.DateTime;
                string kindname = restlistpop.PickerChoices[cbSelect.SelectedIndex].KindName.ToString();
                AddRestPopup(cbSelect.SelectedValue.ToString(), txtReason.Text, dateTime, kindname);
            }
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.Content.XamlRoot;

            await msg.ShowAsync();
        }

        public async void AddRestPopup(string restKind, string reason, DateTime date, string restKindName)
        {
            AddRestPopup arp = new AddRestPopup(restKind, reason, date, restKindName);

            arp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            arp.XamlRoot = this.Content.XamlRoot;

            await arp.ShowAsync();
        }

    }
}








