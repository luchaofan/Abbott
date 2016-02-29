using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PND_ETHICAL_H5PRJECT
    {
        private readonly DAL.PND_ETHICAL_H5PRJECT pnd = new DAL.PND_ETHICAL_H5PRJECT();

        public DataSet GetList(string strWhere)
        {
            return pnd.GetList(strWhere);
        }
    }
}
