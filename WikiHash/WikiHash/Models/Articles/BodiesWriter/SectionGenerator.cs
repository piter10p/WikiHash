using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using WikiHash.Models.Articles.Bodies;
using System.Text.RegularExpressions;

namespace WikiHash.Models.Articles.BodiesWriter
{
    public class SectionGenerator
    {
        Section Section;
        XmlDocument Document;
        XmlElement SectionElement;

        public XmlElement GenerateSection(Section section, XmlDocument document)
        {
            try
            {
                if(section == null || document == null)
                    throw new ArgumentNullException();

                Section = section;
                Document = document;
                SectionElement = Document.CreateElement("section");

                AddTitle();
                AddFrames();

                return SectionElement;
            }
            catch
            {
                throw;
            }
        }

        private void AddTitle()
        {
            var title = Document.CreateElement("title");
            title.InnerText = Section.Title;
            SectionElement.AppendChild(title);
        }

        private void AddFrames()
        {
            var frames = Document.CreateElement("frames");
            SectionElement.AppendChild(frames);

            foreach(var frame in Section.Frames)
            {
                var fr = GetFrame(frame);
                frames.AppendChild(fr);
            }
        }

        private XmlElement GetFrame(ContentFrame contentFrame)
        {
            var frame = Document.CreateElement("frame");

            var width = Document.CreateElement("width");
            width.InnerText = FrameWidthParser.ToString(contentFrame.Width);
            frame.AppendChild(width);

            var content = Document.CreateElement("content");
            var type = Document.CreateAttribute("type");
            type.InnerText = ContentTypeParser.ToString(contentFrame.ContentType);
            content.Attributes.Append(type);

            content.InnerText = contentFrame.Content;
            frame.AppendChild(content);

            return frame;
        }
    }
}