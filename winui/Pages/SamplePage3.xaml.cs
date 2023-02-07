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
using Microsoft.UI.Xaml.Documents;
using winui.ViewModels;
using Vanara;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplePage3 : Page
    {
        string page;
        public SamplePage3()
        {
            this.InitializeComponent();
            PopulateProjects("");
            caldate.SetDisplayDate(DateTime.Now);

            SearchTotalWork("", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void PopulateProjects(string name)
        {
            WorkViewModel viewModel = new WorkViewModel(name);

            this.DataContext = viewModel;
        }

        private void SearchTotalWork(string name, string date)
        {
            WorkTotalViewModel totalViewModel = new WorkTotalViewModel(name, date);
            this.gridview2.DataContext = totalViewModel;
        }


        private async void btnAddWork_Click(object sender, RoutedEventArgs e)
        {
            AddWorkPopup ap = new AddWorkPopup();
            ap.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            ap.XamlRoot = this.XamlRoot;
            
            await ap.ShowAsync();
        }

        private async void btnChangetime_Click(object sender, RoutedEventArgs e)
        {
            page = "change";
            AddWorkerPopup awp = new AddWorkerPopup(page);
            awp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            awp.XamlRoot = this.XamlRoot;

            await awp.ShowAsync();
        }

        private async void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            page = "add";
            AddWorkerPopup awp = new AddWorkerPopup(page);
            awp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            awp.XamlRoot = this.XamlRoot;

            await awp.ShowAsync();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PopulateProjects(txtSearchName.Text);
        }

        private void txtSearchTotal_Click(object sender, RoutedEventArgs e)
        {
            string date = "";
            if (caldate.Date.ToString().Length > 10)
            {
                date = caldate.Date.ToString().Substring(0, 10);
            }
            SearchTotalWork(txtTotalName.Text, date);
        }
    }
}
