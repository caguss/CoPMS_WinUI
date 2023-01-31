using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace winui
{
    public class Project
    {
        public string ProjectNum { get; set; }
        public string ProjectName { get; set; }
        public string Manager { get; set; }
        public string TeamName { get; set; }
        public string CompanyName { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }


        public string ASDate { get; set; }
        public string StandardDate { get; set; } //기준시점
    }

}
