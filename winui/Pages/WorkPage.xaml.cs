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
using System.Diagnostics;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkPage : Page
    {
        DateTime startDate = DateTime.Now;

        public WorkPage()
        {
            this.InitializeComponent();
            PopulateProjects();
        }
        private void PopulateProjects()
        {
            WorkViewModel viewModel = new WorkViewModel();

            this.DataContext = viewModel;
        }


        private async void btnAddWork_Click(object sender, RoutedEventArgs e)
        {
            AddWorkPopup ap = new AddWorkPopup();
            ap.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            ap.XamlRoot = this.XamlRoot;
            ap.Title = "근무 추가";
            ap.PrimaryButtonText = "출근";
            ap.SecondaryButtonText = "퇴근";
            await ap.ShowAsync();
        }

        private async void btnChangetime_Click(object sender, RoutedEventArgs e)
        {
            ChangeWorkTime tm = new ChangeWorkTime();
            tm.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            tm.XamlRoot = this.XamlRoot;

            await tm.ShowAsync();
        }

        private async void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerPopup awp = new AddWorkerPopup();
            awp.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            awp.XamlRoot = this.XamlRoot;

            await awp.ShowAsync();
        }
    }

    public class Project1
    {
        public Project1()
        {
            Activities = new ObservableCollection<Activity>();
        }

        public string Name { get; set; }
        public ObservableCollection<Activity> Activities { get; private set; }
    }

    public class Activity
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Project { get; set; }
    }

}
