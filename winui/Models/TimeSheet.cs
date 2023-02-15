using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winui.Models
{
    public class Project
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public string ProjectManagerCode { get; set; } // 담당자코드
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; } //부서코드
        public string BusinessName { get; set; }
        public string BusinessCode { get; set; } //거래처코드
        public string ContractDate { get; set; }
        public string StProject { get; set; }
        public string EndProject { get; set; }
        public string CompleteYN { get; set; } //완료여부
    }

    public class TimeSheetDetail
    {
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }
        public string StartASDate { get; set; }
        public string EndASDate { get; set; }
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string FProjectStDate { get; set; }
        public string FProjectEndDate { get; set; }
        public string StandardDate { get; set; } //기준시점
        public string TeamName { get; set; }
        public string ProjectManager { get; set; }
    }

    public class TimeSheetData
    {
        public string ProjectTime { get; set;}
        public string ProjectManager { get; set;}
        public string Sum { get; set;}
    }

}
