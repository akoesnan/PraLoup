using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using PraLoup.Utilities;

namespace PraLoup.Utilities
{
    public static class HttpRequestHelper
    {
        public static dynamic GetJsonResponse(string url)
        {            
            var jss = new JavaScriptSerializer(); 
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            var json = GetStringResponse(url);
            return jss.Deserialize(json, typeof(object)) as dynamic;
        }
        
        public static StreamReader GetStreamResponse(string url)
        {
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader;
            }
        }

        public static String GetStringResponse(string url)
        {
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
        }
    }
}
