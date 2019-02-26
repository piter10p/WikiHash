using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace WikiHash
{
    public static class Configuration
    {
        private const string ConfFilePath = @"~/configuration.xml";

        public static void Load()
        {
            try
            {
                var doc = LoadConfigFile();
                var root = doc.FirstChild;

                WebsiteName = root.SelectSingleNode("websiteName").InnerText;
            }
            catch(Exception e)
            {
                throw new Exception("Failed to load WikiHash configuration.", e);
            }
            
        }

        public static string WebsiteName { get; private set; }

        private static XmlDocument LoadConfigFile()
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(HostingEnvironment.MapPath(ConfFilePath));
                return doc;
            }
            catch(Exception e)
            {
                throw new Exception("Failed to load configuration file.", e);
            }
        }
    }
}