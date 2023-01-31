// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Data;
using System.Linq;
using winui.popup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplePage1 : Page
    {

        RestListViewModel RLVM = new RestListViewModel();
        RestViewModel restlist = new RestViewModel();
        public SamplePage1()
        {
            this.InitializeComponent();
            RestData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbSelect.SelectedItem == null || txtReason.Text.Length < 1)
            {
                string msg = "연차 종류를 선택해 주세요.";
                PopupMessage(msg);
            }

            else
            {
            }
        }

        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            calview.SetDisplayDate(DateTimeOffset.Now);
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            calview.SelectedDates.Clear();
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }

        private void calview_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var selectedDate = sender.SelectedDates.FirstOrDefault();
            if (selectedDate.ToString("yyyy년 MM월 dd일").Contains("00"))
            {
                return;
            }
            else
            {
                txtDate.Text = selectedDate.ToString("yyyy년 MM월 dd일");
            }
        }

        //연차관리 - 연차현황 데이터 조회
        private void RestData()
        {
            DataTable dt = new DataTable();
            Provider prov = new Provider();
            dt = prov.RestRemain();

            txtUserDay.Text = string.Format("내 연차 : {0}일  / 사용 연차 : {1}일  / 남은 연차 : {2}일", dt.Rows[0]["연차일수"].ToString(), dt.Rows[0]["사용연차"].ToString(), dt.Rows[0]["남은연차"].ToString());
        }

        private void calview_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
        }
    }
}
