using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PND_ETHICAL_H5PRJECT
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [dbo].[PND_ETHICAL_H5PRJECT]");
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" and" + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
