using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class GPS_QuestionnaireSurvey
    {
        private readonly DAL.GPS_QuestionnaireSurvey gqs = new DAL.GPS_QuestionnaireSurvey();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.GPS_QuestionnaireSurvey model)
        {
            return gqs.Add(model);
        }
    }
}
