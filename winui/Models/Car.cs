using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace winui
{
    public class Car : INotifyPropertyChanged
    {

        public string CarName { get; set; }
        public string CarCode { get; set; }
        public string IsDriving { get; set; }
        public string UsedUser { get; set; }
        public string UsedDate { get; set; }
        public string WhereToGo { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
