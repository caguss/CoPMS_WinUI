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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui.popup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddWorkerPopup : ContentDialog
    {
        string selectpage;
        public AddWorkerPopup(string page)
        {
            this.InitializeComponent();
            txtUser.Text = App.loginUser.UserName;
            tpStart.Time = new TimeSpan(9, 00, 00);
            tpEnd.Time = new TimeSpan(18, 00, 00);
            selectpage= page;
            if(page.Equals("add"))
            {
                txtHeader.Text = "근무자 추가";
                btnAdd.Content = "추가";
            }
            else
            {
                btnAdd.Content = "저장";
                txtHeader.Text = "근무 시간 변경";
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectpage.Equals("add"))
                {
                    bool addwork = Provider.AddWorkTime(tpStart.Time.ToString(), tpEnd.Time.ToString());

                    if (addwork)
                    {
                        this.Hide();
                        string msg = "추가 되었습니다";
                        PopupMessage(msg);
                    }
                    else
                    {
                        this.Hide();
                        string msg = "이미 등록되어있습니다.";
                        PopupMessage(msg);
                    }
                }
                else
                {
                    bool changework = Provider.ChangeWorkTime(tpStart.Time.ToString(),tpEnd.Time.ToString());
                    if (changework)
                    {
                        this.Hide();
                        string msg = "저장 되었습니다";
                        PopupMessage(msg);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public async void PopupMessage(string message)
        {
            MessagePopup msg = new MessagePopup(message);

            msg.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            msg.XamlRoot = this.XamlRoot;

            await msg.ShowAsync();
        }
    }
}
