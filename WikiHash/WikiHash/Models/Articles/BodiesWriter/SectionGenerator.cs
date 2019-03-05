using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WikiHash.Models.Articles.Bodies;
using System.Text.RegularExpressions;

namespace WikiHash.Models.Articles.BodiesWriter
{
    public class SectionGenerator
    {
        Section Section;
        XElement SectionElement;

        public XElement GenerateSection(Section section)
        {
            try
            {
                Section = section ?? throw new ArgumentNullException();
                SectionElement = new XElement("section");

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
            var title = new XElement("title");
            title.Value = Section.Title;
            SectionElement.Add(title);
        }

        private void AddFrames()
        {
            var frames = new XElement("frames");
            SectionElement.Add(frames);

            foreach(var frame in Section.Frames)
            {
                var fr = GetFrame(frame);
                frames.Add(fr);
            }
        }

        private XElement GetFrame(ContentFrame contentFrame)
        {
            XElement frame = new XElement("frame");

            XElement width = new XElement("width");
            width.Value = FrameWidthParser.ToString(contentFrame.Width);
            frame.Add(width);

            try//For content with html tags
            {
                var trimmed = Regex.Replace(contentFrame.Content, @"\s\s+", "");
                XElement content = new XElement("content", XElement.Parse(trimmed));
                frame.Add(content);
            }
            catch(Exception e)//For content withour html tags
            {
                XElement content = new XElement("content");
                content.Value = contentFrame.Content;
                frame.Add(content);
            }

            return frame;
        }
    }
}