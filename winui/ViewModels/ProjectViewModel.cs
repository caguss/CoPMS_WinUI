using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winui.Models;

namespace winui.ViewModels
{
    public class ProjectViewModel
    {
        public List<ProjectDetail> Projects { get; set; }

        public ProjectViewModel(string year, string proName, string proManager, string complete)
        {
            Projects = new List<ProjectDetail>();

            DataTable timeSheettable = Provider.SearchTimeSheet(year, proName, proManager, complete);
            for (int i = 0; i < timeSheettable.Rows.Count; i++)
            {
                //Projects.Add(new TimeSheet()
                //{
                //    ProjectNo = timeSheettable.Rows[i]["프로젝트번호"].ToString(),
                //    ProjectName = timeSheettable.Rows[i]["프로젝트이름"].ToString(),
                //    ProjectManager = timeSheettable.Rows[i]["프로젝트담당자"].ToString(),
                //    DepartmentName = timeSheettable.Rows[i]["부서이름"].ToString(),
                //    BusinessName = timeSheettable.Rows[i]["상호명"].ToString(),
                //    ContractDate = timeSheettable.Rows[i]["계약일"].ToString(),
                //    StProject = timeSheettable.Rows[i]["프로젝트시작일"].ToString(),
                //    EndProject = timeSheettable.Rows[i]["프로젝트종료일"].ToString()

                //});
            }
        }
    }

}
