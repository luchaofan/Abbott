using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Recorded_Info
    {
        private readonly DAL.Recorded_Info ri = new DAL.Recorded_Info();
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
            return ri.AddRecorded_Info(Hospital_Code, Project_Code, IP, Isp, Browser, OS);
        }
    }
}
