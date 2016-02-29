using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Abbott
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private readonly BLL.Recorded_Info recorded = new BLL.Recorded_Info();
        private readonly BLL.DW_HospitalUser hu = new BLL.DW_HospitalUser();

        [WebMethod(Description = "添加")]
        public int AddRecorded_Info(string Hospital_Code, string Project_Code, string IP, string Isp, string Browser, string OS)
        {
            return recorded.AddRecorded_Info(Hospital_Code, Project_Code, IP, Isp, Browser, OS);
        }

        [WebMethod(Description = "根据工号查询医院")]
        public List<DW_HospitalUser> GetList(string strWhere)
        {
            DataSet ds = hu.GetList(" PersonCode='" + strWhere + "'");
            List<DW_HospitalUser> list = new List<DW_HospitalUser>();
            DW_HospitalUser dw = null;
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dw = new DW_HospitalUser();
                    dw.Hospital_Code = ds.Tables[0].Rows[i]["Hospital_Code"].ToString();
                    dw.HospitalName = ds.Tables[0].Rows[i]["HospitalName"].ToString();
                    list.Add(dw);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
