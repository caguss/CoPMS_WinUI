using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace winui
{
    class TeamViewModel : ViewModelBase
    {
       
        public TeamViewModel()
        {
            // Sample to pre-load list of records from data server of KVP
            //PickerChoices = GetDataFromServerForDemo("select * from LookupTable where Category = 'demo'");

            DataTable dt = Provider.TeamInfo();
            PickerChoices = new ObservableCollection<Team>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PickerChoices.Add(new Team
                {
                    TeamCode = dt.Rows[i]["부서코드"].ToString(),
                    TeamName = dt.Rows[i]["부서이름"].ToString()
                });
            }

   
            ShowThisRecord = new Team();
            ShowThisRecord.TeamCode = "001";
        }
        public TeamViewModel(string adddata)
        {

            DataTable dt = Provider.TeamInfo();
            PickerChoices = new ObservableCollection<Team>();
            PickerChoices.Add(new Team { TeamCode = "", TeamName = adddata });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PickerChoices.Add(new Team
                {
                    TeamCode = dt.Rows[i]["부서코드"].ToString(),
                    TeamName = dt.Rows[i]["부서이름"].ToString()
                });
            }
            selectedRecord = PickerChoices[0];
            ShowThisRecord = PickerChoices[0];
        }
        // this is the record that has the "ID" column I am trying to bind to
        public Team ShowThisRecord { get; set; }
        //*****************************************
        Team selectedRecord; //selected item object
        public Team SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                if (selectedRecord != value)
                {
                    selectedRecord = value;
                    OnPropertyChanged();
                }
            }
        }
        //*****************************************
        // The picker is bound to this list of possible choices
        public ObservableCollection<Team> PickerChoices { get; set; }
    }
}
