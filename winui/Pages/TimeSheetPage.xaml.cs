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
using winui.ViewModels;
using winui.Models;
using Vanara.Extensions.Reflection;
using System.Data;
using Microsoft.UI.Xaml.Automation.Peers;
using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.UI.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeSheetPage : Page
    {
        List<string> list = new List<string>();
        TimeSheetViewModel timeSheetView;
        DataTable dt;
       
        //DataGridColumn aa = new DataGridColumn();


        public TimeSheetPage()
        {
            this.InitializeComponent();
            calender.SelectedDate = DateTime.Now;
            cbComplete.SelectedIndex = 1;
            SearchTimeSheet();
        }

        private void SearchTimeSheet()
        {
            string year = calender.SelectedDate.Value.ToString("yyyy");
            string proName = txtProName.Text;
            string proManager = txtProManager.Text;
            string complete = "N";
            if (cbComplete.SelectedValue.ToString().Equals("완료"))
                complete = "Y";
            else if (cbComplete.SelectedValue.ToString().Equals("미결제"))
                complete = "C";

            timeSheetView = new TimeSheetViewModel(year, proName, proManager, complete);
            //TimeSheetViewModel timeSheetView = new TimeSheetViewModel(year,proName,proManager,complete);
            this.gridview.DataContext = timeSheetView;
        }

        private void calender_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

        }


        private void gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetText();
        }

        //TimeSheet Detail
        private void SetText()
        {
            //gridview2.Items.Clear();
            if (gridview.SelectedIndex > -1)
            {
                string projectNo = timeSheetView.TimeSheets[gridview.SelectedIndex].ProjectNo.ToString();

                TimeSheetDetailViewModel TDVM = new TimeSheetDetailViewModel(Convert.ToInt32(projectNo));

                //txtProjectName.Text = TDVM.TimeSheetDetails.GetPropertyValue<TimeSheet>(timeSheet.ProjectName).ToString();
                txtProjectName.Text = TDVM.TimeSheetDetails[0].ProjectName.ToString();
                txtStartASDate.Text = TDVM.TimeSheetDetails[0].StartASDate.ToString() + " ~ " + TDVM.TimeSheetDetails[0].EndASDate.ToString();
                txtCompanyName.Text = TDVM.TimeSheetDetails[0].CompanyName.ToString();
                txtFProjectStDate.Text = TDVM.TimeSheetDetails[0].FProjectStDate.ToString() + " ~ " + TDVM.TimeSheetDetails[0].FProjectEndDate.ToString();
                txtProjectDate.Text = TDVM.TimeSheetDetails[0].ProjectStartDate.ToString() + " ~ " + TDVM.TimeSheetDetails[0].ProjectEndDate.ToString();
                txtProjectManager.Text = TDVM.TimeSheetDetails[0].ProjectManager.ToString();
                txtTeamName.Text = TDVM.TimeSheetDetails[0].TeamName.ToString();
                DateTime MonthFirstDay = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                DateTime MonthLastDay = MonthFirstDay.AddMonths(1).AddDays(-1);

                txtStandardDate.Text = MonthLastDay.ToString("yyyy-MM-dd");

                //TimeSheetDataViewModel dataview = new TimeSheetDataViewModel(Convert.ToInt32(projectNo));
                dt = Provider.TimeSheetData(Convert.ToInt32(projectNo));

                //GridViewHeaderItem d = new GridViewHeaderItem();

                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //     //list.Add(dt.Columns[i].ColumnName);
                //     gridview2.Items.Add(dt.Columns[i].ColumnName);
                //    //gridview2.DataContext = dt.Columns[i].ColumnName;
                //}
                //gridview2.ItemsSource= dt.DefaultView;

                var collection = new ObservableCollection<object>();
                var collectionHeader = new ObservableCollection<object>();

                //collection.Add(dt);
                List<string> list = new List<string>();
               
                datagrid.Columns.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("여부"))
                    { }
                    else
                    {
                      
                        DataGridTextColumn dq = new DataGridTextColumn();
                        collection.Add(dt.Columns[i]);
                        dq.Header = dt.Columns[i].ColumnName;
                        //datagrid.Columns.Add(dq);
                        //dq.Binding = dt;


                        datagrid.Columns.Add(new DataGridTextColumn
                        {
                            Header = dq.Header,
                            Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                        });
                        
                        }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName.Contains("여부")) { }
                        else
                        {
                            collection.Add(dr.ItemArray[i]);  
                        }
                    }
                }
                //gridview2.Header = collectionHeader.ToArray();

                //DataGridRow d = new DataGridRow();
                //d.DataContext = dt.DefaultView;
                //datagrid.DataContext = d;

                //datagrid.DataContext = dt;
                datagrid.ItemsSource = collection;
                gridview2.ItemsSource = collection;
               
               
              //  datagrid.ItemsSource = dt.DefaultView;

                //gridview2.DataContext = dt.DefaultView;
                //gridview2.ItemsSource = dt.AsDataView();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchTimeSheet();
        }

        private void txtProName_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                btnSearch_Click(sender, e);
        }

        private void txtProManager_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                btnSearch_Click(sender, e);
        }

        private void gridview2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
           
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datagrid_CurrentCellChanged(object sender, EventArgs e)
        {

        }
    }
}
