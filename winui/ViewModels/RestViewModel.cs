using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace winui
{
    class RestViewModel : ViewModelBase
    {

        public RestViewModel()
        {
            // Sample to pre-load list of records from data server of KVP
            //PickerChoices = GetDataFromServerForDemo("select * from LookupTable where Category = 'demo'");
            PickerChoices = new ObservableCollection<Restkind>() {
                new Restkind{ RestCode= "A1",KindName="반차/조퇴" },
                new Restkind{ RestCode= "A2",KindName="연차"},
                new Restkind{ RestCode= "A11",KindName="재택근무" },
                new Restkind{ RestCode= "A6",KindName="예비군(동원훈련)" },
                new Restkind{ RestCode= "A7",KindName="예비군(향방작계/기본)" },
                new Restkind{ RestCode= "A8",KindName="민방위훈련" }
        };


            // set the default selected item 
            // foreach (TestModel model in PickerChoices) {
            //     if (model.MyID == 18) {// default value
            //         SelectedRecord = model;
            //         break;
            //     }
            // }
            ShowThisRecord = new Restkind();
            // for grins, I am setting the value that SHOULD be defaulted 
            // in picker.  In this case, ID = 12 = "Some Item" from above
            ShowThisRecord.RestCode = "A1";
        }
        // this is the record that has the "ID" column I am trying to bind to
        public Restkind ShowThisRecord { get; set; }
        //*****************************************
        Restkind selectedRecord; //selected item object
        public Restkind SelectedRecord
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
        public ObservableCollection<Restkind> PickerChoices { get; set; }
    }
}
