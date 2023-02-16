using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using winui.Models;

namespace winui.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }




        public List<Project> Projects { get; set; }

        public ProjectViewModel()
        {
            Projects = new List<Project>();

            DataTable result = Provider.ProjectManager_info_S3("0", "0", "N", "");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                Projects.Add(new Project()
                {
                    ProjectNo = result.Rows[i]["프로젝트번호"].ToString(),
                    ProjectName = result.Rows[i]["프로젝트이름"].ToString(),
                    ProjectManager = result.Rows[i]["프로젝트담당자"].ToString(),
                    DepartmentName = result.Rows[i]["부서이름"].ToString(),
                    BusinessName = result.Rows[i]["상호명"].ToString(),
                    ContractDate = result.Rows[i]["계약일"].ToString(),
                    StProject = result.Rows[i]["프로젝트시작일"].ToString(),
                    EndProject = result.Rows[i]["프로젝트종료일"].ToString(),
                    BusinessCode = result.Rows[i]["거래처코드"].ToString(),
                    CompleteYN = result.Rows[i]["완료여부"].ToString(),
                    ProjectManagerCode = result.Rows[i]["프로젝트담당자코드"].ToString(),
                    DepartmentCode = result.Rows[i]["부서코드"].ToString(),
                    SubCount = result.Rows[i]["하도급갯수"].ToString(),
                     SubPrice = result.Rows[i]["하도급총금액"].ToString(),
                     ODMCount = result.Rows[i]["외주프로젝트인원수"].ToString(),
                });
            }
        }

        public void Refresh(string vDate, string teamcode, string iscomplete, string projname)
        {
            DataTable result = Provider.ProjectManager_info_S3(vDate,teamcode, iscomplete, projname);
            Projects = new List<Project>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                Projects.Add(new Project()
                {
                    ProjectNo = result.Rows[i]["프로젝트번호"].ToString(),
                    ProjectName = result.Rows[i]["프로젝트이름"].ToString(),
                    ProjectManager = result.Rows[i]["프로젝트담당자"].ToString(),
                    DepartmentName = result.Rows[i]["부서이름"].ToString(),
                    BusinessName = result.Rows[i]["상호명"].ToString(),
                    ContractDate = result.Rows[i]["계약일"].ToString(),
                    StProject = result.Rows[i]["프로젝트시작일"].ToString(),
                    EndProject = result.Rows[i]["프로젝트종료일"].ToString(),
                    BusinessCode = result.Rows[i]["거래처코드"].ToString(),
                    CompleteYN = result.Rows[i]["완료여부"].ToString(),
                    ProjectManagerCode = result.Rows[i]["프로젝트담당자코드"].ToString(),
                    DepartmentCode = result.Rows[i]["부서코드"].ToString(),
                    SubCount = result.Rows[i]["하도급갯수"].ToString(),
                    SubPrice = result.Rows[i]["하도급총금액"].ToString(),
                    ODMCount = result.Rows[i]["외주프로젝트인원수"].ToString(),
                });
            }

            OnPropertyChanged();
        }
    }

}
