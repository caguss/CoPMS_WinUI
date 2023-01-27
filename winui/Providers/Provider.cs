using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winui.Providers
{
    public static class Provider
    {
        static string connectionStr = "Test";

        public static DataTable Login(string username, string password, string platform)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UserLogin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            //// 파라메터 선언
            cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.VarChar, 20));
            cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.VarChar, 200));
            cmd.Parameters.Add(new SqlParameter("@Platform", SqlDbType.VarChar, 10));

            //// 값할당
            cmd.Parameters["@userID"].Value = username;
            cmd.Parameters["@Password"].Value = password;



            cmd.Parameters["@Platform"].Value = platform;
            cmd.Parameters["@Token"].Value = "";

            DataSet ds = new DataSet();

            // DB처리
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
            }

            return ds.Tables[0];
        }

        public static string WorkInOut(int userID, bool isStartWork)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "WorkInOut_IU10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                //// 파라메터 선언
                cmd.Parameters.Add(new SqlParameter("@사용자코드", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Isworkstart", SqlDbType.Bit));

                //// 값할당
                cmd.Parameters["@사용자코드"].Value = userID.ToString();
                cmd.Parameters["@Isworkstart"].Value = isStartWork;
                // DB처리
                using (con)
                {
                    con.Open();
                    var data = cmd.ExecuteScalar();
                    return data == null ? "" : data.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static DataSet WorkTime_R10(int usercode, DateTime date)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "WorkTime_R10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                //// 파라메터 선언
                cmd.Parameters.Add(new SqlParameter("@사용자코드", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@검색주", SqlDbType.DateTime));

                //// 값할당
                cmd.Parameters["@사용자코드"].Value = usercode;
                cmd.Parameters["@검색주"].Value = date;

                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static DataTable CarInfo()
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarInfo_R10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }


        }

        public static DataTable CarStatus()
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarRunInfo_S";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 10));
                cmd.Parameters["@회사코드"].Value = "1000";


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        //운행시작
        public static bool CarRegister(string CarCode)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarRunInfo_I";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@등록자", SqlDbType.Int));
                cmd.Parameters["@관리번호"].Value = CarCode;
                cmd.Parameters["@등록자"].Value = App.loginUser.UserID;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //운행종료
        public static bool CarRegisterFIN(string carCode, int distance, string park, int gas, bool refuel, string startloc, string endloc, int fuelprice)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarRunInfo_U1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@누적주행거리", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@주차장소", SqlDbType.VarChar, 5));
                cmd.Parameters.Add(new SqlParameter("@주유상태", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@주유여부", SqlDbType.Bit));
                cmd.Parameters.Add(new SqlParameter("@수정자", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@등록자", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@출발지", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@도착지", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@주유금액", SqlDbType.Int));

                cmd.Parameters["@누적주행거리"].Value = distance;
                cmd.Parameters["@주차장소"].Value = park;
                cmd.Parameters["@주유상태"].Value = gas;
                cmd.Parameters["@주유여부"].Value = refuel;
                cmd.Parameters["@수정자"].Value = App.loginUser.UserID;
                cmd.Parameters["@관리번호"].Value = carCode;
                cmd.Parameters["@등록자"].Value = App.loginUser.UserID;
                cmd.Parameters["@출발지"].Value = startloc;
                cmd.Parameters["@도착지"].Value = endloc;
                cmd.Parameters["@주유금액"].Value = fuelprice;

                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static DataTable CarReservation_Check(DateTime date)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarReservation_S";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Parma_RDate", SqlDbType.Date));
                cmd.Parameters["@Parma_RDate"].Value = date;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable CarUsed_Check(string carcode, DateTime startdate, DateTime enddate)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Car_History";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 3));
                cmd.Parameters.Add(new SqlParameter("@운행일자1", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@운행일자2", SqlDbType.Date));
                cmd.Parameters["@관리번호"].Value = carcode;
                cmd.Parameters["@운행일자1"].Value = startdate;
                cmd.Parameters["@운행일자2"].Value = enddate;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable RestRemain()
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_S4";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@사용자코드", SqlDbType.Int));
                cmd.Parameters["@사용자코드"].Value = App.loginUser.UserID;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable RestRegister(string restkind, DateTime date, string reason)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_IU";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@목적지", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 5));
                cmd.Parameters["@예약일자"].Value = date;
                cmd.Parameters["@관리번호"].Value = restkind;
                cmd.Parameters["@사용자이름"].Value = App.loginUser.UserID;
                cmd.Parameters["@목적지"].Value = reason;
                cmd.Parameters["@회사코드"].Value = "1000";


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable RestAccept1(string restkind, DateTime date, string register)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_IU_2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@로그인", SqlDbType.VarChar, 5));
                cmd.Parameters["@예약일자"].Value = date;
                cmd.Parameters["@관리번호"].Value = restkind;
                cmd.Parameters["@사용자이름"].Value = register;
                cmd.Parameters["@회사코드"].Value = "1000";
                cmd.Parameters["@로그인"].Value = App.loginUser.UserID;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable RestAccept2(string restkind, DateTime date, string register)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_IU_3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@로그인", SqlDbType.VarChar, 5));
                cmd.Parameters["@예약일자"].Value = date;
                cmd.Parameters["@관리번호"].Value = restkind;
                cmd.Parameters["@사용자이름"].Value = register;
                cmd.Parameters["@회사코드"].Value = "1000";
                cmd.Parameters["@로그인"].Value = App.loginUser.UserID;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }

                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Restdelete(string restkind, DateTime date, string register)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_IU_4";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@로그인", SqlDbType.VarChar, 5));
                cmd.Parameters["@예약일자"].Value = date;
                cmd.Parameters["@관리번호"].Value = restkind;
                cmd.Parameters["@사용자이름"].Value = register;
                cmd.Parameters["@회사코드"].Value = "1000";
                cmd.Parameters["@로그인"].Value = App.loginUser.UserID;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }

            }
            catch (Exception)
            {

            }
        }
        public static void RestList(List<Rest> reason)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RestList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            // DB처리               
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con.Close();

            DataTable dt = ds.Tables[0]; //연차리스트





            if (dt.Rows.Count == 0)
            { }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Rest rest = new Rest
                    {
                        Reason = " " + dt.Rows[i]["연차사유"].ToString(),
                        UserName = dt.Rows[i]["사용자이름"].ToString(),
                        UserCode = dt.Rows[i]["신청자"].ToString(),
                        RestKind = dt.Rows[i]["관리번호"].ToString(),
                        Date = dt.Rows[i]["연차일자"].ToString()
                    };
                    reason.Add(rest);
                }
            }
            reason.TrimExcess();
        }
        public static void CarList(List<Car> reason)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "CarList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            // DB처리               
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con.Close();

            DataTable dt = ds.Tables[0]; //연차리스트





            if (dt.Rows.Count == 0)
            { }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Car car = new Car
                    {
                        UsedDate = " " + dt.Rows[i]["예약일자"].ToString(),
                        CarCode = dt.Rows[i]["관리번호"].ToString(),
                        CarName = dt.Rows[i]["차종명"].ToString(),
                        UsedUser = dt.Rows[i]["예약자명"].ToString(),
                        WhereToGo = dt.Rows[i]["목적지"].ToString(),
                        IsDriving = "예약중"
                    };
                    reason.Add(car);
                }
            }
            reason.TrimExcess();
        }

        public static DataTable CarReservation(DateTime date, string togo, string carCode)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CarReservation_IU";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 5));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@목적지", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@회사코드", SqlDbType.VarChar, 8));
                cmd.Parameters["@예약일자"].Value = date;
                cmd.Parameters["@관리번호"].Value = carCode;
                cmd.Parameters["@사용자이름"].Value = App.loginUser.UserName;
                cmd.Parameters["@목적지"].Value = togo;
                cmd.Parameters["@회사코드"].Value = "1000";


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable RestDetail(DateTime date, string RestKind, string user)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Leave_S3";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@예약일자", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@관리번호", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@사용자이름", SqlDbType.VarChar, 30));
                cmd.Parameters.Add(new SqlParameter("@로그인", SqlDbType.VarChar, 30));
                cmd.Parameters["@예약일자"].Value = date; //날짜
                cmd.Parameters["@관리번호"].Value = RestKind; //연차종류
                cmd.Parameters["@사용자이름"].Value = user; //신청자
                cmd.Parameters["@로그인"].Value = App.loginUser.UserID; //로그인자


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static DataTable NotiLog(int target, string title, string comment)
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotiSender";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@작성자", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@대상", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@제목", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@내용", SqlDbType.VarChar, 1000));
                cmd.Parameters["@작성자"].Value = App.loginUser.UserID;
                cmd.Parameters["@대상"].Value = target;
                cmd.Parameters["@제목"].Value = title;
                cmd.Parameters["@내용"].Value = comment;


                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable WorkLog()
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "WorkManage_R11";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@사용자코드", SqlDbType.VarChar, 10));
                cmd.Parameters["@사용자코드"].Value = "";

                DataSet ds = new DataSet();

                // DB처리
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
    }
}
