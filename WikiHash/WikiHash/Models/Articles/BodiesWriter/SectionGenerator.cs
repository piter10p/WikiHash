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

            var trimmedContent = Regex.Replace(contentFrame.Content, @"\s\s+", "");
            trimmedContent = Regex.Replace(trimmedContent, @"<br>", "");//TODO: Dirty way to remove <br> objects created by Quill. Becouse they're not closed, they're throwing exception.
            trimmedContent = HTMLSecurityTagsRemover.RemoveUnsecureTags(trimmedContent);
            var content = Document.CreateElement("content");
            content.InnerXml = trimmedContent;
            frame.AppendChild(content);

            return frame;
        }
    }
}