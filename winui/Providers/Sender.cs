
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace winui
{
    class Sender
    {
        public static bool SendNotifications(string Msg, string token, string AcceptKey, string title)
        {
            try
            {
                if (token != null)
                {
                    var FCMTockenValue = token; //알람을 보낼 핸드폰 주소값
                    FCMBody body = new FCMBody(); //알람을 보낼 데이터
                    FCMBody_ios body1 = new FCMBody_ios(); //알람을 보낼 데이터
                    FCMNotification _notification = new FCMNotification();
                    _notification.title = title;
                    _notification.body = Msg;
                    FCMData data = new FCMData();
                    data.key1 = App.loginUser.UserID.ToString(); // 
                    data.key2 = AcceptKey; //승인분기
                    data.key3 = "코에버 알림"; // 제목
                    data.key4 = Msg;
                    body.registration_ids = new[] { FCMTockenValue };
                    body._notification = _notification;
                    body.data = data;
                    body1.registration_ids = new[] { FCMTockenValue };
                    body1.notification = _notification;
                    body1.data = data;
                    var isSuccessCall = SendNotification(body).Result;
                    var isSuccessCall1 = SendNotification(body1).Result;
                    if (isSuccessCall && isSuccessCall1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> SendNotifications(string Msg,string title, string token)
        {
            try
            {
                var FCMTockenValue = token; //알람을 보낼 핸드폰 주소값
                FCMBody body = new FCMBody(); //알람을 보낼 데이터
                FCMBody_ios body1 = new FCMBody_ios(); //알람을 보낼 데이터
                FCMNotification _notification = new FCMNotification();
                _notification.title = title;
                _notification.body = Msg;
                FCMData data = new FCMData();
                data.key1 = App.loginUser.UserID.ToString(); // 
                data.key2 = ""; 
                data.key3 = "코에버 공지알림"; // 제목
                data.key4 = Msg;
                body.registration_ids = new[] { FCMTockenValue };
                body._notification = _notification;
                body.data = data;
                body1.registration_ids = new[] { FCMTockenValue };
                body1.notification = _notification;
                body1.data = data;
                SendNotification(body);
                SendNotification(body1);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static async Task<bool> SendNotification(FCMBody fcmBody)
        {
            try
            {
                var httpContent = JsonConvert.SerializeObject(fcmBody);
                var client = new HttpClient();
                var authorization = string.Format("key={0}", "AAAAiYHMN7Y:APA91bEIgea4T0znA9Y0xOv0UsDt-Cmz9tOZD_5r0C49sGEFvqZQIa6poBhp5uoKjJvTvSHpvrpSxXll8I_OuZj48KXgrcyaXjvvlRCat37KkzgNYaSHZeIS4Fy650anQNzuizhFvdH-"); // 서버주소(바뀔예정)
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                var stringContent = new StringContent(httpContent);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string uri = "https://fcm.googleapis.com/fcm/send";
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                var result = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private static async Task<bool> SendNotification(FCMBody_ios fcmBody)
        {
            try
            {
                var httpContent = JsonConvert.SerializeObject(fcmBody);
                var client = new HttpClient();
                var authorization = string.Format("key={0}", "AAAAiYHMN7Y:APA91bEIgea4T0znA9Y0xOv0UsDt-Cmz9tOZD_5r0C49sGEFvqZQIa6poBhp5uoKjJvTvSHpvrpSxXll8I_OuZj48KXgrcyaXjvvlRCat37KkzgNYaSHZeIS4Fy650anQNzuizhFvdH-"); // 서버주소(바뀔예정)
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                var stringContent = new StringContent(httpContent);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string uri = "https://fcm.googleapis.com/fcm/send";
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                var result = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
