using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace winui
{
    class GoToWorkViewModel
    {
        public string Time { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }

        public string InSun { get; set; }
        public string InMon { get; set; }
        public string InTue { get; set; }
        public string InWed { get; set; }
        public string InThu { get; set; }
        public string InFri { get; set; }
        public string InSat { get; set; }
        public string OutSun { get; set; }
        public string OutMon { get; set; }
        public string OutTue { get; set; }
        public string OutWed { get; set; }
        public string OutThu { get; set; }
        public string OutFri { get; set; }
        public string OutSat { get; set; }
        public bool IsStartWork { get; set; } //출근 상황인지
        public string SelectedWeek { get; set; } // 선택 주


        public GoToWorkViewModel(DateTime date)
        {
           
            DataSet ds = Provider.WorkTime_R10(Convert.ToInt32(App.loginUser.UserID), date);
            DataTable dt_WeekWorkTime = ds.Tables[0];
            DataTable dt_SelectedWeek = ds.Tables[1];
            DataTable dt_WorkToday = ds.Tables[2];
            Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            if (dt_WeekWorkTime.Rows.Count > 0)
            {
                for (int i = 0; i < dt_WeekWorkTime.Rows.Count; i++)
                {
                    switch (dt_WeekWorkTime.Rows[i]["요일"].ToString())
                    {
                        case "1": //일요일
                            InSun = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutSun = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "2":
                            InMon = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutMon = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "3":
                            InTue = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutTue = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "4":
                            InWed = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutWed = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "5":
                            InThu = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutThu = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "6":
                            InFri = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutFri = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                        case "7": //토요일
                            InSat = dt_WeekWorkTime.Rows[i]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["출근시간"]).ToString("HH:mm");
                            OutSat = dt_WeekWorkTime.Rows[i]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WeekWorkTime.Rows[i]["퇴근시간"]).ToString("HH:mm");
                            break;
                    }
                }
            }
            if (dt_SelectedWeek.Rows.Count > 0)
            {
                SelectedWeek = dt_SelectedWeek.Rows[0]["검색주"].ToString();

            }
            if (dt_WorkToday.Rows.Count > 0)
            {
                StartWork = dt_WorkToday.Rows[0]["출근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WorkToday.Rows[0]["출근시간"]).ToString("HH:mm");
                EndWork = dt_WorkToday.Rows[0]["퇴근시간"].ToString() == "" ? "" : Convert.ToDateTime(dt_WorkToday.Rows[0]["퇴근시간"]).ToString("HH:mm");

                if (StartWork == "")
                    IsStartWork = true;
                else
                    IsStartWork = false;
            }


        }

    }
}
