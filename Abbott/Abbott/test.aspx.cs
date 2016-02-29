using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Abbott
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string data = "{\"DbHost\": \"ABT_SVC\",\"WfpUser\": \"SVC_SIS_U\",\"AppKey\": \"cbab8470e67ad67e3fb46885b5f9db23\",\"Function\": \"CreateQRCode\",\"Channel_ID\": \"123456789\",                                                   \"Channel\": 0,\"Hospital_Code\": \"" + "Hospital_Code" +
                                   "\",\"MMC_Code\": \"" + "" + "\"}";


            string url = PostByWebRequest(string.Format("http://membercrm.chinacloudapp.cn:3341/api/aggregator/CreateQrCode?a=1"), data);
        }
        //模拟httppost请求
        static System.Net.CookieContainer cookieContainer = null;
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
                request.CookieContainer = cookieContainer;
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
    }

}