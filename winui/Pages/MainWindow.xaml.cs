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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using static PInvoke.User32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
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

        public static int MinWindowWidth { get; set; } = 1000;
        public static int MaxWindowWidth { get; set; } = 1000;
        public static int MinWindowHeight { get; set; } = 800;
        public static int MaxWindowHeight { get; set; } = 800;

        public MainWindow()
        {
            this.InitializeComponent();
            RegisterWindowMinMax(this);
            
            
        }
        private static void RegisterWindowMinMax(Window window)
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
      
        private void ColorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {

            //_clicks += 1;
            //Control1Output.Text = "Number of clicks: " + _clicks;
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
                    if (this.frame.Navigate(typeof(Home)))
                    {
                        sender.Header = "Home";
                    }
                    string itemTag = item.Tag.ToString();

                    //sender.Header = "샘플 페이지 " + itemTag.Substring(itemTag.Length - 1);
                    if(itemTag.Contains("1"))
                    {
                        sender.Header = "연차관리";
                    }
                   else if(itemTag.Contains("2"))
                    {
                        sender.Header = "차량예약관리";
                    }
                    else if(itemTag.Contains("3"))
                    {
                        sender.Header = "근태관리";
                    } 
                    else if(itemTag.Contains("4"))
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

        private void navigationViewItem2_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.grid.RequestedTheme = (this.grid.RequestedTheme == ElementTheme.Dark) ? ElementTheme.Light : ElementTheme.Dark;
        }

        #endregion

        //#region 설정 표시 여부 체크 박스 클릭시 처리하기 - isSettingsVisibleCheckBox_Click(sender, e)

        ///// <summary>
        ///// 설정 표시 여부 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void isSettingsVisibleCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    this.navigationView.IsSettingsVisible = (sender as CheckBox).IsChecked == true ? true : false;
        //}

        //#endregion
        //#region 뒤로가기 버튼 표시 체크 박스 클릭시 처리하기 - backButtonVisibleCheckBox_Click(sender, e)

        ///// <summary>
        ///// 뒤로가기 버튼 표시 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void backButtonVisibleCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    if (checkBox.IsChecked == true)
        //    {
        //        this.navigationView.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Visible;
        //    }
        //    else
        //    {
        //        this.navigationView.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
        //    }
        //}

        //#endregion
        //#region 뒤로가기 버튼 이용 가능 체크 박스 클릭시 처리하기 - backButtonEnabledCheckBox_Click(sender, e)

        ///// <summary>
        ///// 뒤로가기 버튼 이용 가능 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void backButtonEnabledCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    this.navigationView.IsBackEnabled = (sender as CheckBox).IsChecked == true ? true : false;
        //}

        //#endregion
        //#region 자동 제안 박스 체크 박스 클릭시 처리하기 - autoSuggestBoxCheckBox_Click(sender, e)

        ///// <summary>
        ///// 자동 제안 박스 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void autoSuggestBoxCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    if (checkBox.IsChecked == true)
        //    {
        //        AutoSuggestBox autoSuggestBox = new AutoSuggestBox()
        //        {
        //            QueryIcon = new SymbolIcon(Symbol.Find)
        //        };

        //        this.navigationView.AutoSuggestBox = autoSuggestBox;
        //    }
        //    else
        //    {
        //        this.navigationView.AutoSuggestBox = null;
        //    }
        //}

        //#endregion
        //#region 헤더 항상 표시 체크 박스 클릭시 처리하기 - alwaysShowHeaderCheckBox_Click(sender, e)

        ///// <summary>
        ///// 헤더 항상 표시 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void alwaysShowHeaderCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    this.navigationView.AlwaysShowHeader = checkBox.IsChecked == true ? true : false;
        //}

        //#endregion
        //#region 창 커스텀 컨텐트 표시 체크 박스 클릭시 처리하기 - showPaneCustomContentCheckBox_Click(sender, e)

        ///// <summary>
        ///// 창 커스텀 컨텐트 표시 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void showPaneCustomContentCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    if (checkBox.IsChecked == true)
        //    {
        //        this.moreInformationHyperlinkButton.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        this.moreInformationHyperlinkButton.Visibility = Visibility.Collapsed;
        //    }
        //}

        //#endregion
        //#region 창 꼬리말 표시 체크 박스 클릭시 처리하기 - showPaneFooterCheckBox_Click(sender, e)

        ///// <summary>
        ///// 창 꼬리말 표시 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void showPaneFooterCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    if (checkBox.IsChecked == true)
        //    {
        //        this.paneFooterStackPanel.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        this.paneFooterStackPanel.Visibility = Visibility.Collapsed;
        //    }
        //}

        //#endregion
        //#region 왼쪽 창 위치 라디오 버튼 체크시 처리하기 - leftPanePositionCheckBox_Checked(sender, e)

        ///// <summary>
        ///// 왼쪽 창 위치 라디오 버튼 체크시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void leftPanePositionCheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.navigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left;
        //    this.navigationView.IsPaneOpen = true;

        //    this.paneFooterStackPanel.Orientation = Orientation.Vertical;
        //}

        //#endregion
        //#region 위쪽 창 위치 라디오 버튼 체크시 처리하기 - topPanePositionRadioButton_Checked(sender, e)

        ///// <summary>
        ///// 위쪽 창 위치 라디오 버튼 체크시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void topPanePositionRadioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.navigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top;
        //    this.navigationView.IsPaneOpen = false;

        //    this.paneFooterStackPanel.Orientation = Orientation.Horizontal;
        //}

        //#endregion
        //#region 포커스 변경시 선택 변경 체크 박스 클릭시 처리하기 - selectionFollowsFocusCheckBox_Click(sender, e)

        ///// <summary>
        ///// 포커스 변경시 선택 변경 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void selectionFollowsFocusCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    if (checkBox.IsChecked == true)
        //    {
        //        this.navigationView.SelectionFollowsFocus = Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus.Enabled;
        //    }
        //    else
        //    {
        //        this.navigationView.SelectionFollowsFocus = Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus.Disabled;
        //    }
        //}

        //#endregion
        //#region 메뉴 항목 2 선택 금지 체크 박스 클릭시 처리하기 - suppressMenuItem2SelectionCheckBox_Click(sender, e)

        ///// <summary>
        ///// 메뉴 항목 2 선택 금지 체크 박스 클릭시 처리하기
        ///// </summary>
        ///// <param name="sender">이벤트 발생자</param>
        ///// <param name="e">이벤트 인자</param>
        //private void suppressMenuItem2SelectionCheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    CheckBox checkBox = sender as CheckBox;

        //    this.navigationViewItem2.SelectsOnInvoked = (checkBox.IsChecked == true) ? false : true;
        //}

        //#endregion
    }
}
