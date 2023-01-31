using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace winui
{ 
  
    class RestListViewModel
    {
        public CultureInfo Culture => new CultureInfo("ko-KR");

        public RestListViewModel()
        {
            List<Rest> rests = new List<Rest>();
         
            Provider prov = new Provider();
         
            try
            {
                prov.RestList(rests);
                for (int i = 0; i < rests.Count; i++)
                {
                    List<Rest> findrests = rests.FindAll(x => x.Date.Contains(rests[i].Date)); //해당날짜 리스트 검색
                    for (int j = 0; j < findrests.Count; j++)
                    {
                        switch (findrests[j].RestKind)
                        {
                            case "A11": // 재택
                                findrests[j].RestKind_Word = "재택근무";
                               
                                break;
                            case "A2": // 연차
                            case "A10": // 출산휴가
                            case "A12": // 특별연차
                            case "A30:": // 대체휴가
                            case "A3:": // 직계가족사망
                            case "A4:": // 본인결혼
                            case "A5:": // 형제/자매결혼
                            case "A9:": // 출산휴가
                                findrests[j].RestKind_Word = "연차";
                           
                                break;
                            case "A1": // 반차
                            case "A20": // 팀조기퇴근
                                findrests[j].RestKind_Word = "반차";
                           
                                break;
                            case "A6": // 예비군(동원)
                            case "A7": // 예비군(향방작계)
                            case "A8": // 민방위
                                findrests[j].RestKind_Word = "동원";
                             
                                break;
                        }
                    }
                    
                   
                }
            }
            catch (Exception)
            {
            }

        
        }

    }
}
