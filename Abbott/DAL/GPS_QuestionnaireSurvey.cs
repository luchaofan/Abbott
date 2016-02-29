using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GPS_QuestionnaireSurvey
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.GPS_QuestionnaireSurvey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GPS_QuestionnaireSurvey(");
            strSql.Append("CharacterofStool,Duration,Crying,OtherSymptoms,YinaiSymptoms1,YinaiSymptoms2,YinaiSymptoms3,YinaiSymptoms4,YinaiSymptoms5,YinaiSymptoms6,DateofBirth,SymptomsofCrying1,SymptomsofCrying2,SymptomsofCrying3,SymptomsofCrying4,SymptomsofCrying5,CreateTime,BirthWeight,ModeofDelivery,Born,GestationalWeek,FeedingWay,DefecationFrequency,DefecationTime");
            strSql.Append(") values (");
            strSql.Append("@CharacterofStool,@Duration,@Crying,@OtherSymptoms,@YinaiSymptoms1,@YinaiSymptoms2,@YinaiSymptoms3,@YinaiSymptoms4,@YinaiSymptoms5,@YinaiSymptoms6,@DateofBirth,@SymptomsofCrying1,@SymptomsofCrying2,@SymptomsofCrying3,@SymptomsofCrying4,@SymptomsofCrying5,@CreateTime,@BirthWeight,@ModeofDelivery,@Born,@GestationalWeek,@FeedingWay,@DefecationFrequency,@DefecationTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CharacterofStool", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@Duration", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@Crying", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@OtherSymptoms", SqlDbType.NVarChar,350) ,            
                        new SqlParameter("@YinaiSymptoms1", SqlDbType.NVarChar,300) ,            
                        new SqlParameter("@YinaiSymptoms2", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@YinaiSymptoms3", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@YinaiSymptoms4", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@YinaiSymptoms5", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@YinaiSymptoms6", SqlDbType.NVarChar,350) ,            
                        new SqlParameter("@DateofBirth", SqlDbType.DateTime) ,            
                        new SqlParameter("@SymptomsofCrying1", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@SymptomsofCrying2", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@SymptomsofCrying3", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@SymptomsofCrying4", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@SymptomsofCrying5", SqlDbType.NVarChar,350) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@BirthWeight", SqlDbType.Int,4) ,            
                        new SqlParameter("@ModeofDelivery", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@Born", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@GestationalWeek", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@FeedingWay", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@DefecationFrequency", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@DefecationTime", SqlDbType.NVarChar,150)             
              
            };

            parameters[0].Value = model.CharacterofStool;
            parameters[1].Value = model.Duration;
            parameters[2].Value = model.Crying;
            parameters[3].Value = model.OtherSymptoms;
            parameters[4].Value = model.YinaiSymptoms1;
            parameters[5].Value = model.YinaiSymptoms2;
            parameters[6].Value = model.YinaiSymptoms3;
            parameters[7].Value = model.YinaiSymptoms4;
            parameters[8].Value = model.YinaiSymptoms5;
            parameters[9].Value = model.YinaiSymptoms6;
            parameters[10].Value = model.DateofBirth;
            parameters[11].Value = model.SymptomsofCrying1;
            parameters[12].Value = model.SymptomsofCrying2;
            parameters[13].Value = model.SymptomsofCrying3;
            parameters[14].Value = model.SymptomsofCrying4;
            parameters[15].Value = model.SymptomsofCrying5;
            parameters[16].Value = model.CreateTime;
            parameters[17].Value = model.BirthWeight;
            parameters[18].Value = model.ModeofDelivery;
            parameters[19].Value = model.Born;
            parameters[20].Value = model.GestationalWeek;
            parameters[21].Value = model.FeedingWay;
            parameters[22].Value = model.DefecationFrequency;
            parameters[23].Value = DateTime.Now;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }
    }
}
