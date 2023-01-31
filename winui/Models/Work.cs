using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace winui
{
    public class Work
    {
        private DateTime workStartTime;
        private DateTime workEndTime;
        private DateTime workDate;

        public string UserName { get; set; }
        public string WorkStartTime { get => workStartTime.ToString("HH:mm:ss"); set => workStartTime = Convert.ToDateTime(value); }
        public string WorkEndTime { get => workStartTime.ToString("HH:mm:ss"); set => workEndTime = Convert.ToDateTime(value); }
        public string WorkDate { get => workStartTime.ToString("yyyy-MM-dd"); set => workDate = Convert.ToDateTime(value); }


    }


}
