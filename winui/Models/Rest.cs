using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
//using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml.Linq;

namespace winui
{


    public class Rest
    {
        // Instance members.
        

        public string Reason { set; get; }
        
        public string UserName { set; get; }
        public string UserCode { set; get; }
        public string RestKind { set; get; }
        public string Date { set; get; }
        public string RestKind_Word {  set; get; }

        // Static members.
        public Rest()
        {
           
        }

        static Rest()
        {
          

            List<Rest> all = new List<Rest>();

            var directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filename = System.IO.Path.Combine(directory.ToString(), "LogData.xml");

            try // 기존 로그 데이터 불러옴
            {
                if (File.Exists(filename))
                {
                   
                        XDocument xdev = XDocument.Load(filename);
                        foreach (XElement item in xdev.Descendants("log"))
                        {
                            string title = item.Element("title").Value;
                            string body = item.Element("body").Value;

                            Rest rest = new Rest
                            {
                                UserName = title,
                                Reason = body
                            };

                            all.Add(rest);

                        }
                 
                }
                else
                {
                    Rest rest = new Rest
                    {
                        Reason = "알람이 존재하지 않습니다. \n아래로 당겨 새로고침 할 수 있습니다."
                    };

                    all.Add(rest);
                }
            }
           
            catch (Exception)
            {

            }

            all.TrimExcess();
            All = all;
        }

        public static IEnumerable<Rest> All { set; get; }
        public static List<Rest> Rests { set; get; }

    }
}

