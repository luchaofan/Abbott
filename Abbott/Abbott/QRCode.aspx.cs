using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace Abbott
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void download_Click(object sender, EventArgs e)
        {
            this.download.Enabled = false;//禁用
            string personCode = pCode.Value;
            string loginName;
            string path = Server.MapPath("/") + "erweima\\";
            BLL.DW_HospitalUser dw_hu = new BLL.DW_HospitalUser();
            PinyinHelper pinyin = new PinyinHelper();
            DataSet ds1 = dw_hu.GetList(" personCode='" + personCode + "'");//查询用户对应的所有医院
            if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows != null)
            {
                loginName = ds1.Tables[0].Rows[0]["LoginName"].ToString();//用户名,pinyin.convertCh汉字转拼音
                if (!File.Exists(path + loginName + ".zip"))
                {
                    Directory.CreateDirectory(path + loginName);

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        // string fileName = ds1.Tables[0].Rows[i]["Hospital_Code"] + "_" + pinyin.convertCh(ds1.Tables[0].Rows[i]["HospitalName"].ToString());//医院CODE_医院名称
                        string fileName = ds1.Tables[0].Rows[i]["Hospital_Code"] + "_" + ds1.Tables[0].Rows[i]["HospitalName"];//医院CODE_医院名称
                        Directory.CreateDirectory(path + loginName + "\\" + fileName);//在用户名下创建文件夹

                        BLL.PND_ETHICAL_H5PRJECT pnd = new PND_ETHICAL_H5PRJECT();
                        DataSet ds = pnd.GetList("");
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows != null)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {

                                string data = "{\"DbHost\": \"ABT_SVC\",\"WfpUser\": \"SVC_SIS_U\",\"AppKey\": \"cbab8470e67ad67e3fb46885b5f9db23\",\"Function\": \"CreateQRCode\",\"Channel_ID\": \"123456789\",                                                   \"Channel\": 0,\"Hospital_Code\": \"" + ds.Tables[0].Rows[j]["PROJECTCODE"].ToString() + "@" + ds1.Tables[0].Rows[i]["Hospital_Code"].ToString() +
                                    "\",\"MMC_Code\": \"" + "" + "\"}";




                                string url = PostByWebRequest(string.Format("http://membercrm.chinacloudapp.cn:3341/api/aggregator/CreateQrCode?a=1"), data);
                                string hospitalName = ds1.Tables[0].Rows[i]["HospitalName"].ToString();
                                string projectName = ds.Tables[0].Rows[j]["PROJECTNAME"].ToString();
                                string proName = ds.Tables[0].Rows[j]["PROJECTCODE"] + "_" + hospitalName;//二维码名称以"项目CODE_项目名称"命名
                                JavaScriptSerializer json = new JavaScriptSerializer();
                                WebchatImg webchatImg = json.Deserialize<WebchatImg>(url);
                                string strUrl = webchatImg.Url;

                                CreateQrCode(path + loginName + "\\" + fileName + "\\" + proName + ".jpg", ds1.Tables[0].Rows[i]["Hospital_Code"].ToString(),
                                    ds.Tables[0].Rows[j]["PROJECTCODE"].ToString(), strUrl, hospitalName, projectName);
                            }
                        }
                    }
                    string[] filesOrDirectoriesPaths = new string[] { path + loginName };
                    //生成压缩包
                    CreateZip(path + loginName + ".zip", path, 6, filesOrDirectoriesPaths);
                    Directory.Delete(path + loginName, true);
                    DownLoad(path, loginName + ".zip");//下载
                }
                else
                {
                    DownLoad(path, loginName + ".zip");//下载
                }
            }
            this.download.Enabled = true;//启用
        }

        //生成二维码并保存
        public void CreateQrCode(string picPath, string hospitalCode, string projectCode, string strUrl, string hospitalName, string projectName)
        {
            SignInCodeHelper helper = new SignInCodeHelper();
            //本地测试地址
            //string url = string.Format("http://192.168.1.101:803/pages/Index.html?h_Code={0}&p_Code={1}&url={2}", hospitalCode, projectCode, strUrl);//医院CODE、项目CODE和url
            //服务器测试地址
            string url = string.Empty;
            if (projectCode == "P0002")
            {
                //医院CODE、项目CODE和url
                url = string.Format("http://ethicalh5website.chinacloudsites.cn/pages/Index.html?h_Code={0}&p_Code={1}&url={2}", hospitalCode, projectCode, strUrl);
            }
            else
            {
                url = "http://ethicalh5website.chinacloudsites.cn/NotFound.aspx";
            }

            string picPath1 = Server.MapPath("/") + "erweima\\shuiyin_img\\" + projectCode + ".jpg";

            Bitmap img = helper.GetSignInCode(url);
            Bitmap img2 = helper.ImageWatermarkToRiget(projectName, img, picPath1);
            Bitmap img3 = helper.ImageWatermark(img, picPath1, projectName);

            HttpContext.Current.Response.ContentType = "image/jpeg";

            img3.Save(picPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            img.Dispose();
            img2.Dispose();
            img3.Dispose();
            //File.Delete(picPath1);
            // HttpContext.Current.Response.End();

        }

        //生成压缩包
        public void CreateZip(string strZipPath, string strZipTopDirectoryPath, int intZipLevel, string[] filesOrDirectoriesPaths)
        {
            ZipHelper zip = new ZipHelper();
            zip.Zip(strZipPath, strZipTopDirectoryPath, 6, "", filesOrDirectoriesPaths);
            //  zip.CreateZip(strZipTopDirectoryPath, strZipPath);
        }

        //下载
        public void DownLoad(string Path, string zipName)
        {
            string strResult = string.Empty;
            //  string strPath = Server.MapPath("~/uploadfile/");
            string strFile = string.Format(@"{0}\{1}", Path, zipName);

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(zipName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }

        }

        //模拟httppost请求
        static CookieContainer cookieContainer = null;
        public static string PostByWebRequest(string URI, string postString)
        {
            try
            {
                // 设置打开页面的参数 
                HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
                // 接收返回的页面
                //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //System.IO.Stream responseStream = response.GetResponseStream();
                //System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
                //string srcString = reader.ReadToEnd();
                byte[] postData = Encoding.ASCII.GetBytes(postString); // 将提交的字符串数据转换成字节数组
                // 设置提交的相关参数
                request = WebRequest.Create(URI) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = "application/json";
                request.CookieContainer = null;
                request.ContentLength = postData.Length;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                // 提交请求数据
                System.IO.Stream outputStream = request.GetRequestStream();
                outputStream.Write(postData, 0, postData.Length);
                outputStream.Close();
                // 接收返回的页面
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                System.IO.Stream responseStream = response.GetResponseStream();
                responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                string srcString = reader.ReadToEnd();
                return srcString;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [Serializable]
        public class WebchatImg
        {
            public string Url { get; set; }
            public string Result { get; set; }
            public string Remark { get; set; }
            public string Error { get; set; }
        }
    }
}