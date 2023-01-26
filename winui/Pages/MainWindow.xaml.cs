// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using Vanara.PInvoke;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static int MinWindowWidth { get; set; } = 1000;
        public static int MaxWindowWidth { get; set; } = 1000;
        public static int MinWindowHeight { get; set; } = 800;
        public static int MaxWindowHeight { get; set; } = 800;

        public MainWindow()
        {
            this.InitializeComponent();
            RegisterWindowMinMax(this);
          
        }

        private async void Login()
        {
            if(Properties.Resources.IsLoginYN.Equals("N"))
            {
                Login login = new Login();

                login.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;

                login.XamlRoot = this.main.XamlRoot;

                await login.ShowAsync();
            }
        }
      
        #region 네비게이션 뷰 선택 변경시 처리하기 - navigationView_SelectionChanged(sender, e)

        /// <summary>
        /// 네비게이션 뷰 선택 변경시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void navigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs e)
        {

            if (e.IsSettingsSelected)
            {
                this.frame.Navigate(typeof(SettingPage));
                sender.Header = "설정";
            }
            else
            {
                Microsoft.UI.Xaml.Controls.NavigationViewItem item = e.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;

                if (item != null)
                {
                    //if (this.frame.Navigate(typeof(Home)))
                    //{
                    //    sender.Header = "Home";
                    //}
                    string itemTag = item.Tag.ToString();

                    //sender.Header = "샘플 페이지 " + itemTag.Substring(itemTag.Length - 1);
                    if (itemTag.Contains("1"))
                    {
                        sender.Header = "연차관리";
                    }
                    else if (itemTag.Equals("Home"))
                    {
                        sender.Header = "Home";
                    }
                    else if (itemTag.Contains("2"))
                    {
                        sender.Header = "차량예약관리";
                    }
                    else if (itemTag.Contains("3"))
                    {
                        sender.Header = "근태관리";
                    }
                    else if (itemTag.Contains("4"))
                    {
                        sender.Header = "타임시트관리";
                    }
                    else if (itemTag.Contains("5"))
                    {
                        sender.Header = "프로젝트관리";
                    }

                    string pageName = "winui." + itemTag;

                    Type pageType = Type.GetType(pageName);

                    this.frame.Navigate(pageType);
                }
            }
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem select)
            {
                this.Hide();
            }
        }

        #endregion

        #region 화면 크기 변경
        private static WinProc newWndProc = null;
        private static IntPtr oldWndProc = IntPtr.Zero;
        private delegate IntPtr WinProc(IntPtr hWnd, WindowMessage Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        internal static extern int GetDpiForWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, WindowLongIndexFlags nIndex, WinProc newProc);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, WindowLongIndexFlags nIndex, WinProc newProc);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, WindowMessage Msg, IntPtr wParam, IntPtr lParam);


        public static void RegisterWindowMinMax(Window window)
        {

            var hwnd = GetWindowHandleForCurrentWindow(window);

            newWndProc = new WinProc(WndProc);
            oldWndProc = SetWindowLongPtr(hwnd, WindowLongIndexFlags.GWL_WNDPROC, newWndProc);
        }

        private static IntPtr GetWindowHandleForCurrentWindow(object target) =>
            WinRT.Interop.WindowNative.GetWindowHandle(target);

        private static IntPtr WndProc(IntPtr hWnd, WindowMessage Msg, IntPtr wParam, IntPtr lParam)
        {
            switch (Msg)
            {
                case WindowMessage.WM_GETMINMAXINFO:
                    var dpi = GetDpiForWindow(hWnd);
                    var scalingFactor = (float)dpi / 96;

                    var minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                    minMaxInfo.ptMinTrackSize.x = (int)(MinWindowWidth * scalingFactor);
                    minMaxInfo.ptMaxTrackSize.x = (int)(MaxWindowWidth * scalingFactor);
                    minMaxInfo.ptMinTrackSize.y = (int)(MinWindowHeight * scalingFactor);
                    minMaxInfo.ptMaxTrackSize.y = (int)(MaxWindowHeight * scalingFactor);

                    Marshal.StructureToPtr(minMaxInfo, lParam, true);
                    break;

            }
            return CallWindowProc(oldWndProc, hWnd, Msg, wParam, lParam);
        }

        private static IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongIndexFlags nIndex, WinProc newProc)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, newProc);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, newProc));
        }

        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [Flags]
        private enum WindowLongIndexFlags : int
        {
            GWL_WNDPROC = -4,
        }

        private enum WindowMessage : int
        {
            WM_GETMINMAXINFO = 0x0024,
        }
        #endregion

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            Login();
        }
    }
}
