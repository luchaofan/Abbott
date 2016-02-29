using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DW_HospitalUser
    {
        /// <summary>
        /// 获取用户、医院信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct  Hospital_Code,PersonCode,HospitalName,eu.LoginName from [dbo].[DW_HospitalUser] hu");
            strSql.Append(" inner join [dbo].[DW_SIS_Hospital] su on hu.HOspital_Code=su.HOspitalCode");
            strSql.Append(" inner join [dbo].[DW_EthicalUser] eu on hu.PersonCode=eu.LoginCode");
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" and" + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
