using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara;
using winui.Models;

namespace winui.ViewModels
{
    public class WorkTotalViewModel
    {
        public List<WorkTotal> WorkTotalList { get; set; }

        public WorkTotalViewModel(string name , string date)
        {
            WorkTotalList = new List<WorkTotal>();
           
            DataTable worklogtable = Provider.SearchWorkTotal(name,date);
            for (int i = 0; i < worklogtable.Rows.Count; i++)
            {
                WorkTotalList.Add(new WorkTotal()
                {
                    UserName = worklogtable.Rows[i]["사용자이름"].ToString(),
                    WorkStartTime = worklogtable.Rows[i]["출근시간"].ToString(),
                    WorkEndTime = worklogtable.Rows[i]["퇴근시간"].ToString(),
                    WorkDate = worklogtable.Rows[i]["일자"].ToString()
                });
            }
        }
    }
}
