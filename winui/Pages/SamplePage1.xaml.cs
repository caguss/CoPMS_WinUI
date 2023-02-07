// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Data;
using System.Linq;
using Vanara;
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

        string date;
        string today;
        RestViewModel restlist = new RestViewModel();
        public SamplePage1()
        {
            this.InitializeComponent();
            try
            {
                MyRestData();
                datepic.SelectedDate = new DateTimeOffset(DateTime.Today);
                today = DateTime.Now.ToString("yyyy-MM-dd");
                calview.SetDisplayDate(DateTimeOffset.Now);
                RestDayData(today);
               

                //this.DataContext = RLVM.restList;
            }
            catch (Exception ex)
            {
                PopupMessage(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtReason.Text.Length < 1)
            {
                string msg = "연차 사유를 작성해주세요";
                PopupMessage(msg);
            }

            else
            {
                string msg = string.Format("연차 종류 : {0}", cbSelect.Text);
                AddRestPopup(msg);

                DataTable dt = new DataTable();
                DateTime dateTime = datepic.Date.DateTime;

                dt = Provider.RestRegister(cbSelect.SelectedValue.ToString(), dateTime, txtReason.Text);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        string okmsg = "연차 등록이 완료되었습니다";
                        PopupMessage(okmsg);
                        RestDayData(today);
                    }
                }
            }
        }

        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            calview.SetDisplayDate(DateTimeOffset.Now);
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            calview.SelectedDates.Clear();
            RestDayData(today);
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }

        public async void AddRestPopup(string msg)
        {
            AddRestPopup arp = new AddRestPopup(msg);

            arp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            arp.XamlRoot = this.XamlRoot;

            await arp.ShowAsync();
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
                date = selectedDate.ToString("yyyy-MM-dd");
            }

            RestDayData(date);
        }

        //연차관리 - 내 연차현황 데이터 조회
        private void MyRestData()
        {
            DataTable dt = new DataTable();
            dt = Provider.RestRemain();

            txtUserDay.Text = string.Format("내 연차 : {0}일  / 사용 연차 : {1}일  / 남은 연차 : {2}일", dt.Rows[0]["연차일수"].ToString(), dt.Rows[0]["사용연차"].ToString(), dt.Rows[0]["남은연차"].ToString());
        }

        //선택한 날짜 연차 현황
        private void RestDayData(string date)
        {
            RestListViewModel RLVM = new RestListViewModel(date);
            this.DataContext = RLVM;
        }

        private void calview_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
        }

        private void cbSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a =  cbSelect.SelectedValue.ToString();
            string b = cbSelect.ItemsSource.ToString();
        }
    }
}
