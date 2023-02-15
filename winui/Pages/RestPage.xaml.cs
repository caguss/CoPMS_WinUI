// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vanara;
using Vanara.Extensions.Reflection;
using winui.popup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestPage : Page
    {

        string date;
        string today;

        RestViewModel restlist = new RestViewModel();
        public RestPage()
        {
            this.InitializeComponent();
            try
            {
                MyRestData();
                datepic.SelectedDate = new DateTimeOffset(DateTime.Today);
                today = DateTime.Now.ToString("yyyy-MM-dd");
                calview.SetDisplayDate(DateTimeOffset.Now);
                RestDayData(today);
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
                DateTime dateTime = datepic.Date.DateTime;
                string kindname = restlist.PickerChoices[cbSelect.SelectedIndex].KindName.ToString();
                AddRestPopup(cbSelect.SelectedValue.ToString(), txtReason.Text, dateTime, kindname);

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

        public async void AddRestPopup(string restKind, string reason, DateTime date, string restKindName)
        {
            AddRestPopup arp = new AddRestPopup(restKind, reason, date , restKindName);

            arp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            arp.XamlRoot = this.Content.XamlRoot;

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
            string d = restlist.PickerChoices[cbSelect.SelectedIndex].KindName.ToString();
           
            //string d = cbSelect.SelectedValue.GetPropertyValue<Restkind>("KindName").ToString();
        }
    }
}
