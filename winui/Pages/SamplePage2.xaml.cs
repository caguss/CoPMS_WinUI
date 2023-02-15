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
using System.Data;
using static Vanara.PInvoke.CM_PARTIAL_RESOURCE_DESCRIPTOR;
using System.Diagnostics;
using Vanara;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplePage2 : Page
    {

        CarViewModel carlist = new CarViewModel();
        string date;
        string today;
        public SamplePage2()
        {
            InitializeComponent();
            try
            {
                CarList();
                datepic.SelectedDate = new DateTimeOffset(DateTime.Today);
                today = DateTime.Now.ToString("yyyy-MM-dd");
                CarDayData(today);
            }
            catch (Exception ex)
            {
                PopupMessage(ex.Message);
            }   
        }

        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            calview2.SetDisplayDate(DateTimeOffset.Now);
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            calview2.SelectedDates.Clear();
            CarDayData(today);
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            if (cbCar.SelectedItem == null || txtloc.Text.Length < 1)
            {
                string message = "목적지를 입력해주세요.";
                PopupMessage(message);
            }
            else
            {
                DateTime dateTime = datepic.Date.DateTime;
             //   string msg = "예약이 완료되었습니다";
                string carKindName = carlist.PickerChoices[cbCar.SelectedIndex].CarName.ToString();
                ReserveCarPopup(cbCar.SelectedValue.ToString(), txtloc.Text,dateTime, carKindName);
            
              //  CarDayData(today);
            }
        }

        private void btnStDrive_Click(object sender, RoutedEventArgs e)
        {
            if (cbCarName.SelectedItem == null)
            {
                string msg = "차량을 선택해주세요.";
                PopupMessage(msg);
            }

            else 
            {
                Provider.CarRegister(cbCarName.SelectedItem.ToString());
                string start = "운행이 시작되었습니다.";

                PopupMessage(start);
            }
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }

        #region 차량 리스트
        private void CarList()
        {
            DataTable dt;
            dt = Provider.CarStatus();
            if (dt.Rows[0]["주차장소"].ToString() == "사용")
                spark.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[0]["차종명"].ToString(), dt.Rows[0]["주차장소"].ToString(), dt.Rows[0]["사용자이름"].ToString());
            else
                spark.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[0]["차종명"].ToString(), dt.Rows[0]["주차장소"].ToString(), dt.Rows[0]["사용자이름"].ToString(), dt.Rows[0]["주유상태"].ToString());

            if (dt.Rows[1]["주차장소"].ToString() == "사용")
                orlando.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[1]["차종명"].ToString(), dt.Rows[1]["주차장소"].ToString(), dt.Rows[1]["사용자이름"].ToString());
            else
                orlando.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[1]["차종명"].ToString(), dt.Rows[1]["주차장소"].ToString(), dt.Rows[1]["사용자이름"].ToString(), dt.Rows[1]["주유상태"].ToString());

            if (dt.Rows[2]["주차장소"].ToString() == "사용")
                genesis.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[2]["차종명"].ToString(), dt.Rows[2]["주차장소"].ToString(), dt.Rows[2]["사용자이름"].ToString());
            else
                genesis.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[2]["차종명"].ToString(), dt.Rows[2]["주차장소"].ToString(), dt.Rows[2]["사용자이름"].ToString(), dt.Rows[2]["주유상태"].ToString());


            if (dt.Rows[3]["주차장소"].ToString() == "사용")
                korando.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[3]["차종명"].ToString(), dt.Rows[3]["주차장소"].ToString(), dt.Rows[3]["사용자이름"].ToString());
            else
                korando.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[3]["차종명"].ToString(), dt.Rows[3]["주차장소"].ToString(), dt.Rows[3]["사용자이름"].ToString(), dt.Rows[3]["주유상태"].ToString());

            if (dt.Rows[4]["주차장소"].ToString() == "사용")
                sorento.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[4]["차종명"].ToString(), dt.Rows[4]["주차장소"].ToString(), dt.Rows[4]["사용자이름"].ToString());
            else
                sorento.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[4]["차종명"].ToString(), dt.Rows[4]["주차장소"].ToString(), dt.Rows[4]["사용자이름"].ToString(), dt.Rows[4]["주유상태"].ToString());

            if (dt.Rows[5]["주차장소"].ToString() == "사용")
                trailblazer.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[5]["차종명"].ToString(), dt.Rows[5]["주차장소"].ToString(), dt.Rows[5]["사용자이름"].ToString());
            else
                trailblazer.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[5]["차종명"].ToString(), dt.Rows[5]["주차장소"].ToString(), dt.Rows[5]["사용자이름"].ToString(), dt.Rows[5]["주유상태"].ToString());

            if (dt.Rows[6]["주차장소"].ToString() == "사용")
                santafe.Text = string.Format("{0}→ {1} [{2}]", dt.Rows[6]["차종명"].ToString(), dt.Rows[6]["주차장소"].ToString(), dt.Rows[6]["사용자이름"].ToString());
            else
                santafe.Text = string.Format("{0}→ {1} [{2}] 주유량: {3}%", dt.Rows[6]["차종명"].ToString(), dt.Rows[6]["주차장소"].ToString(), dt.Rows[6]["사용자이름"].ToString(), dt.Rows[6]["주유상태"].ToString());
        }
        #endregion

        private void calview2_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
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
            CarDayData(date);
        }

        private void calview2_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
        }

        //선택한 날짜의 예약 차량 조회
        private void CarDayData(string date)
        {
            CarListViewModel CLVM = new CarListViewModel(date);
            this.DataContext= CLVM;
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
