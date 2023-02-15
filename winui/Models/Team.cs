using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace winui
{
    public class Team : INotifyPropertyChanged
    {

        public string TeamName { get; set; }
        public string TeamCode { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
