using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DBUtility;

namespace DAL
{
    public class Recorded_Info
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Hospital_Code"></param>
        /// <param name="Project_Code"></param>
        /// <param name="IP"></param>
        /// <param name="Isp"></param>
        /// <param name="Browser"></param>
        /// <param name="OS"></param>
        /// <returns></returns>
        public int AddRecorded_Info(string Hospital_Code, string Project_Code, string IP, string Isp, string Browser, string OS)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into [dbo].[Recorded_Info](Hospital_Code,Project_Code,IP,Isp,Browser,OS,Jointime)");
            strSql.Append(" values(@Hospital_Code,@Project_Code,@IP,@Isp,@Browser,@OS,@Jointime)");
            SqlParameter[] para = new SqlParameter[] {
           new SqlParameter{ParameterName="@Hospital_Code",Value=Hospital_Code},
           new SqlParameter{ParameterName="@Project_Code",Value=Project_Code},
           new SqlParameter{ParameterName="@IP",Value=IP},
           new SqlParameter{ParameterName="@Isp",Value=Isp},
           new SqlParameter{ParameterName="@Browser",Value=Browser},
           new SqlParameter{ParameterName="@OS",Value=OS},
           new SqlParameter{ParameterName="@Jointime",Value=DateTime.Now}
           };
            return DbHelperSQL.ExecuteSql(strSql.ToString(), para);
        }
    }
}
