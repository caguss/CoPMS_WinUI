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
using System.Data;
using winui.ViewModels;
using winui.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectPage : Page
    {

        TeamViewModel TVM; //for ProjectMaster
        TeamViewModel TVM_Detail; //for ProjectDetail
        ProjectViewModel PVM;
        ProjectDetailModel PDM;
        public ProjectPage()
        {
            this.InitializeComponent();
            TVM = new TeamViewModel("전체");
            TVM_Detail = new TeamViewModel();
            PVM = new ProjectViewModel();
            PDM = new ProjectDetailModel();
            cb_Team.DataContext = TVM;
            cb_Team2.DataContext = TVM_Detail;
        }




        /// <summary>
        /// 조회버튼 클릭 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PVM.Refresh
                (dp_Date.Date == null ? "0" : dp_Date.Date.Value.ToString("yyyy-MM"),
                TVM.SelectedRecord.TeamCode,cb_completeYN.IsChecked.Value?"Y":"N",txt_Projname.Text
                );
            listviewMain.ItemsSource = PVM.Projects;

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count >0)
            {
                DataTable result = Provider.ProjectManager_info_S4(((Project)e.AddedItems[0]).ProjectNo, ((Project)e.AddedItems[0]).ProjectManagerCode);

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (result.Rows[i]["프로젝트담당자"].ToString() == result.Rows[i]["사용자코드"].ToString())
                    {
                        PDM = new ProjectDetailModel(result);
                        break;
                    }
                }
                PDM.SubCount = ((Project)e.AddedItems[0]).SubCount;
                PDM.ODMCount = ((Project)e.AddedItems[0]).ODMCount;
                PDM.ProjectName = ((Project)e.AddedItems[0]).ProjectName;
                TVM_Detail.FindSelect(PDM.TeamCode);

                this.DataContext = PDM;
            }
        }
    }
}
