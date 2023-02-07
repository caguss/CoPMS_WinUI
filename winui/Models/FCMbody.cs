using System;
using System.Collections.Generic;
using System.Text;

namespace winui
{
    public class FCMBody

    {

        public string[] registration_ids { get; set; }

        public FCMNotification _notification { get; set; }

        public FCMData data { get; set; }

    }
    public class FCMBody_ios
    {
        public string[] registration_ids { get; set; }

        public FCMNotification notification { get; set; }

        public FCMData data { get; set; }
    }
    public class FCMNotification

    {

        public string body { get; set; }

        public string title { get; set; }

    }

    public class FCMData

    {

        public string key1 { get; set; }

        public string key2 { get; set; }

        public string key3 { get; set; }

        public string key4 { get; set; }

    }
}
