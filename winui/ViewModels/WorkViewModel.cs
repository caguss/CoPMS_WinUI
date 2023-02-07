using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace winui.ViewModels
{
    public class WorkViewModel
    {
        public List<Work> WorkLogList { get; set; }
        public List<Work> StandardWorkList { get; set; }
        public WorkViewModel(string name)
        {
            WorkLogList = new List<Work>();
            StandardWorkList = new List<Work>();
            DataTable worklogtable = Provider.SearchWorkLog(name);
            for (int i = 0; i < worklogtable.Rows.Count; i++)
            {
                WorkLogList.Add(new Work()
                {
                    UserName = worklogtable.Rows[i]["사용자이름"].ToString(),
                    WorkStartTime = worklogtable.Rows[i]["출근시간"].ToString(),
                    WorkEndTime = worklogtable.Rows[i]["퇴근시간"].ToString()
                });
            }
        }
    }
}
