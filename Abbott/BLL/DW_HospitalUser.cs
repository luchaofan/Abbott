using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DW_HospitalUser
    {
        private readonly DAL.DW_HospitalUser dw_hu = new DAL.DW_HospitalUser();

        /// <summary>
        /// 获取用户、医院信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            return dw_hu.GetList(strWhere);
        }
    }
}
