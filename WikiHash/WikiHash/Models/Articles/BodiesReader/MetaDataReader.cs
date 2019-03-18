using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.Bodies;
using System.Xml;

namespace WikiHash.Models.Articles.BodiesReader
{
    public static class MetaDataReader
    {
        public static MetaData Read(XmlNode metaNode)
        {
            try
            {
                var tags = metaNode.SelectSingleNode("tags").InnerText;

                return new MetaData(tags);
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read metadata.", e);
            }
        }
    }
}