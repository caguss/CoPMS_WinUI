using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using winui.Models;

namespace winui
{

    /// <summary>
    /// ProjectModel은 TimeSheet 안에 있음
    /// 차후 이동 가능함.
    /// </summary>
    public class ProjectDetailModel : INotifyPropertyChanged
    {
        private string isCompleteYN = "N";

        public string ProjectNum { get; set; } //프로젝트번호
        public string ProjectName { get; set; } //프로젝트이름
        public string Manager { get; set; } //프로젝트담당자
        public string TeamCode { get; set; } //부서코드
        public string UserName { get; set; }  //사용자이름
        public string ContractName { get; set; } //상호명
        public string ContractCode { get; set; } //거래처코드
        public string ProjectCost { get; set; } //프로젝트금액
        public DateTime ContractDate { get; set; } = DateTime.Now; //계약일
        public DateTime ProjectStartDate { get; set; } = DateTime.Now; //프로젝트시작일
        public DateTime ProjectEndDate { get; set; } = DateTime.Now; //프로젝트종료일


        public DateTime ASStartDate { get; set; } = DateTime.Now; //집중AS기간시작일
        public DateTime ASEndDate { get; set; } = DateTime.Now; //집중AS기간종료일
        public string SubCount { get; set; } //하도급
        public string ODMCount { get; set; } //외주인력
        public string ProjectMemo { get; set; } //프로젝트담당자메모
        public bool IsCompleteYN
        {
            get
            {
                if (isCompleteYN == "N")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (value)
                {
                    isCompleteYN = "Y";
                }
                else
                {
                    isCompleteYN = "N";
                }
            }
        } //완료여부

        public ProjectDetailModel()
        {

        }

        public ProjectDetailModel(DataTable result)
        {
            ProjectNum = result.Rows[0]["프로젝트번호"].ToString();
            Manager = result.Rows[0]["프로젝트담당자"].ToString();
            TeamCode = result.Rows[0]["부서코드"].ToString();
            UserName = result.Rows[0]["사용자이름"].ToString();
            ContractName = result.Rows[0]["상호명"].ToString();
            ContractCode = result.Rows[0]["거래처코드"].ToString();
            ProjectCost = result.Rows[0]["프로젝트금액"].ToString();
            ContractDate = result.Rows[0]["계약일"].ToString() == "" ?
                 DateTime.Now : Convert.ToDateTime(result.Rows[0]["계약일"].ToString());
            ProjectStartDate = result.Rows[0]["프로젝트시작일"].ToString() == "" ?
                 DateTime.Now : Convert.ToDateTime(result.Rows[0]["프로젝트시작일"].ToString());
            ProjectEndDate = result.Rows[0]["프로젝트종료일"].ToString() == "" ?
                DateTime.Now : Convert.ToDateTime(result.Rows[0]["프로젝트종료일"].ToString());
            ASStartDate = result.Rows[0]["집중AS기간시작일"].ToString() == "" ?
                DateTime.Now : Convert.ToDateTime(result.Rows[0]["집중AS기간시작일"].ToString());
            ASEndDate = result.Rows[0]["집중AS기간종료일"].ToString() == "" ?
                 DateTime.Now : Convert.ToDateTime(result.Rows[0]["집중AS기간종료일"].ToString());
            ProjectMemo = result.Rows[0]["프로젝트담당자메모"].ToString();
            isCompleteYN = result.Rows[0]["완료여부"].ToString();

            OnPropertyChanged();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



    }


}
