using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace winui
{

    class CarListViewModel
    {
    
        public CultureInfo Culture => new CultureInfo("ko-KR");
        public List<Car> reservations { get; set; }

        public List<Car> carList { get; set; }  
        public CarListViewModel(string date)
        {
            //Car car= new Car();
            reservations = new List<Car>();
            List<Car> findcars = new List<Car>();
            //car.UsedDate = date;
            try
            {
                Provider.CarList(reservations);
                for (int i = 0; i < reservations.Count; i++)
                {
                    findcars = reservations.FindAll(x => x.UsedDate.Contains(date)); //해당날짜 리스트 검색
                }
                carList = findcars;
            }

            catch (Exception)
            {
            }

        }

        public string SearchReservationable(DateTime date, string carcode)
        {
            List<Car> findcars = reservations.FindAll(x => x.UsedDate.Contains(date.ToString("yyyy-MM-dd")));
            for (int i = 0; i < findcars.Count; i++)
            {
                if (findcars[i].CarCode == carcode)
                {
                    if (findcars[i].UsedUser == App.loginUser.UserName)
                        return "예약 취소";
                    else
                        return "차량 예약 중";
                }
            }
            return "차량 예약";
        }
    }

}
