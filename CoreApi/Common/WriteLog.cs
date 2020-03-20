using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Common
{
    public class WriteLog
    {
        private readonly IConfiguration _configuration;
        public WriteLog(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void WriteGetRequestParm(string ActionName, object[] Parm)
        {
            if (Parm == null)
                return;

            string RequestParmPath = _configuration.GetValue<string>("RequestParmPath");
            string Request = "";

            foreach (object ar in Parm)
            {
                Request = "" + '"' + ar + '"' + ',';
            }

            Request = "[{" + Request.Trim(',') + "}]";
            using (StreamWriter sw = new StreamWriter(string.Format(RequestParmPath, DateTime.Now.ToString("yyyy-MM-dd")), true, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DATE=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \r\n");
                sb.Append("ActionName=" + ActionName + " \r\n");
                sb.Append("PARM=" + Request + "\r\n");
                sb.Append("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－" + "　\r\n");
                sw.Write(sb.ToString());
            }
        }
        public void WritePostRequestParm(string ActionName, object Parm)
        {
            if (Parm == null)
                return;

            string RequestParmPath = _configuration.GetValue<string>("RequestParmPath");
            string Request = JsonConvert.SerializeObject(Parm);

            using (StreamWriter sw = new StreamWriter(string.Format(RequestParmPath, DateTime.Now.ToString("yyyy-MM-dd")), true, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DATE=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \r\n");
                sb.Append("ActionName=" + ActionName + " \r\n");
                sb.Append("PARM=" + Request + "\r\n");
                sb.Append("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－" + "　\r\n");
                sw.Write(sb.ToString());
            }
        }
        public void WriteGetReponse<T>(string ActionName, IEnumerable<T> result)
        {
            if (result == null)
                return;

            string ReponsePath = _configuration.GetValue<string>("ReponsePath");
            string Reponse = "";

            //int i = 0;
            foreach (T ar in result)
            {
                string Line = "";
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ar);
                foreach (PropertyDescriptor prop in properties)
                {
                    string FullName = prop.PropertyType.FullName;
                    if (FullName == "System.Int32" || FullName == "System.Decimal")
                        Line += "" + '"' + prop.Name + '"' + ":" + prop.GetValue(ar) + ',';
                    else if (FullName == "System.DateTime")
                        Line += "" + '"' + prop.Name + '"' + ":" + '"' + Convert.ToDateTime(prop.GetValue(ar)).ToString("yyyy/MM/dd HH:mm:ss") + '"' + ",";
                    else
                        Line += "" + '"' + prop.Name + '"' + ":" + '"' + prop.GetValue(ar) + '"' + ",";
                }
                Reponse += ",{" + Line.Trim(',') + "}" + " \r\n";
                //i++;
            }

            Reponse = "[" + Reponse.TrimStart(',') + "]";
            using (StreamWriter sw = new StreamWriter(string.Format(ReponsePath, DateTime.Now.ToString("yyyy-MM-dd")), true, Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DATE=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \r\n");
                sb.Append("ActionName=" + ActionName + " \r\n");
                sb.Append("PARM=" + Reponse + " \r\n");
                sb.Append("－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－" + "　\r\n");
                sw.Write(sb.ToString());
            }
        }
    }
}
