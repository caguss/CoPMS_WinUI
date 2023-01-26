using H.NotifyIcon;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winui.Models
{
    public class TrayIcon : TaskbarIcon
    {
        public string item1 { get; set; }
        public string item2 { get; set; }

        public TrayIcon()
        {
            item1 = "sfds";
            item2 = "sggsd";
        }

   

    }
}
