using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winui.Models;

namespace winui.ViewModels
{
    public class TimeSheetViewModel
    {
        public List<TimeSheet> TimeSheets { get; set; }

        public TimeSheetViewModel(string year, string proName, string proManager, string complete)
        {
            TimeSheets = new List<TimeSheet>();

            DataTable timeSheettable = Provider.SearchTimeSheet(year, proName, proManager, complete);
            for (int i = 0; i < timeSheettable.Rows.Count; i++)
            {
                TimeSheets.Add(new TimeSheet()
                {
                    ProjectNo = timeSheettable.Rows[i]["프로젝트번호"].ToString(),
                    ProjectName = timeSheettable.Rows[i]["프로젝트이름"].ToString(),
                    ProjectManager = timeSheettable.Rows[i]["프로젝트담당자"].ToString(),
                    DepartmentName = timeSheettable.Rows[i]["부서이름"].ToString(),
                    BusinessName = timeSheettable.Rows[i]["상호명"].ToString(),
                    ContractDate = timeSheettable.Rows[i]["계약일"].ToString(),
                    StProject = timeSheettable.Rows[i]["프로젝트시작일"].ToString(),
                    EndProject = timeSheettable.Rows[i]["프로젝트종료일"].ToString()

                });
            }
        }
    }

    public class TimeSheetDetailViewModel
    {
        public List<TimeSheetDetail> TimeSheetDetails { get; set; }

        public TimeSheetDetailViewModel(int projectNo)
        {
            TimeSheetDetails = new List<TimeSheetDetail>();

            DataTable dt = Provider.TimeSheetDetail(projectNo);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TimeSheetDetails.Add(new TimeSheetDetail()
                {
                    ProjectName = dt.Rows[i]["프로젝트이름"].ToString(),
                    ProjectManager = dt.Rows[i]["프로젝트담당자이름"].ToString(),
                    CompanyName = dt.Rows[i]["상호명"].ToString(),
                    StartASDate = dt.Rows[i]["집중AS기간시작일"].ToString(),
                    EndASDate = dt.Rows[i]["집중AS기간종료일"].ToString(),
                    ProjectStartDate = dt.Rows[i]["프로젝트시작일"].ToString(),
                    ProjectEndDate = dt.Rows[i]["프로젝트종료일"].ToString(),
                    FProjectStDate = dt.Rows[i]["최초프로젝트시작일"].ToString(),
                    FProjectEndDate = dt.Rows[i]["최초프로젝트종료일"].ToString(),
                    TeamName = dt.Rows[i]["부서코드"].ToString()
                });
            }
        }
    }

    public class TimeSheetDataViewModel
    {
        public List<TimeSheetData> TimeSheetDatas { get; set; }

        public TimeSheetDataViewModel(int projectNo)
        {
            TimeSheetDatas = new List<TimeSheetData>();

            //DataTable dt = Provider.TimeSheetData(projectNo);
            DataTable dt = Provider.TimeSheetData(projectNo);

            
          
        }
    }
}
