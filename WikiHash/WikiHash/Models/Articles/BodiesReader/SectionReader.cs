using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using WikiHash.Models.Articles.Bodies;

namespace WikiHash.Models.Articles.BodiesReader
{
    public static class SectionReader
    {
        public static List<Section> Read(XmlNode sectionsNode)
        {
            try
            {
                List<Section> sections = new List<Section>();

                foreach(XmlNode node in sectionsNode.ChildNodes)
                {
                    var section = ReadSectionNode(node);
                    sections.Add(section);
                }

                return sections;
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read sections of article.", e);
            }
        }

        private static Section ReadSectionNode(XmlNode node)
        {
            try
            {
                var title = node.SelectSingleNode("title").InnerText;
                var frames = new List<ContentFrame>();

                var framesNodes = node.SelectSingleNode("frames").ChildNodes;

                foreach(XmlNode frameNode in framesNodes)
                {
                    var frame = ReadFrameNode(frameNode);
                    frames.Add(frame);
                }

                return new Section(title, frames);
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read section.", e);
            }
        }

        private static ContentFrame ReadFrameNode(XmlNode node)
        {
            try
            {
                var text = node.SelectSingleNode("content").InnerText;
                var width = node.SelectSingleNode("width").InnerText;
                return new ContentFrame(text, width);
            }
            catch(Exception e)
            {
                throw new Exception("Falied to read frame.", e);
            }
        }
    }
}