using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace winui
{
    class CarViewModel : ViewModelBase
    {
       
        public CarViewModel()
        {
            // Sample to pre-load list of records from data server of KVP
            //PickerChoices = GetDataFromServerForDemo("select * from LookupTable where Category = 'demo'");

            Provider prov = new Provider();
            DataTable dt = prov.CarInfo();
            PickerChoices = new ObservableCollection<Car>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PickerChoices.Add(new Car
                {
                    CarCode = dt.Rows[i]["관리번호"].ToString(),
                    CarName = $"{dt.Rows[i]["차종명"].ToString()}({dt.Rows[i]["차량번호"].ToString()})"
                });
            }

        //    PickerChoices = new ObservableCollection<Car>() {
        //        new Car{ CarCode = "001",CarName="스파크(42머8691)"}, 
        //        new Car{ CarCode = "004",CarName="올란도(24가1847)"},
        //        new Car{ CarCode = "005",CarName="제네시스(162하7800)"},
        //        new Car{ CarCode = "006",CarName="코란도(145하6688)"},
        //        new Car{ CarCode = "008",CarName="쏘렌토(191허8639)"}, 
        //        new Car{ CarCode = "009",CarName="트레일블레이저(192호3648)"}, 
        //        new Car{ CarCode = "011",CarName="싼타페(172허7890)"}
        //      };


            // set the default selected item 
            // foreach (TestModel model in PickerChoices) {
            //     if (model.MyID == 18) {// default value
            //         SelectedRecord = model;
            //         break;
            //     }
            // }
            ShowThisRecord = new Car();
            // for grins, I am setting the value that SHOULD be defaulted 
            // in picker.  In this case, ID = 12 = "Some Item" from above
            ShowThisRecord.CarCode = "001";
        }
        public CarViewModel(string adddata)
        {

            Provider prov = new Provider();
            DataTable dt = prov.CarInfo();
            PickerChoices = new ObservableCollection<Car>();
            PickerChoices.Add(new Car { CarCode = "", CarName = adddata });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PickerChoices.Add(new Car
                {
                    CarCode = dt.Rows[i]["관리번호"].ToString(),
                    CarName = $"{dt.Rows[i]["차종명"].ToString()}({dt.Rows[i]["차량번호"].ToString()})"
                });
            }

            // Sample to pre-load list of records from data server of KVP
            //PickerChoices = GetDataFromServerForDemo("select * from LookupTable where Category = 'demo'");
            //PickerChoices = new ObservableCollection<Car>() {
            //    new Car{ CarCode = "",CarName=adddata},
            //    new Car{ CarCode = "001",CarName="스파크(42머8691)"},
            //    new Car{ CarCode = "004",CarName="올란도(24가1847)"},
            //    new Car{ CarCode = "005",CarName="제네시스(162하7800)"},
            //    new Car{ CarCode = "006",CarName="코란도(145하6688)"},
            //    new Car{ CarCode = "008",CarName="쏘렌토(191허8639)"},
            //    new Car{ CarCode = "009",CarName="트레일블레이저(192호3648)"},
            //    new Car{ CarCode = "011",CarName="싼타페(172허7890)"}
        //};


            // set the default selected item 
            // foreach (TestModel model in PickerChoices) {
            //     if (model.MyID == 18) {// default value
            //         SelectedRecord = model;
            //         break;
            //     }
            // }
            selectedRecord = PickerChoices[0];
            ShowThisRecord = PickerChoices[0];
            // for grins, I am setting the value that SHOULD be defaulted 
            // in picker.  In this case, ID = 12 = "Some Item" from above
        }
        // this is the record that has the "ID" column I am trying to bind to
        public Car ShowThisRecord { get; set; }
        //*****************************************
        Car selectedRecord; //selected item object
        public Car SelectedRecord
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
        public ObservableCollection<Car> PickerChoices { get; set; }
    }
}
