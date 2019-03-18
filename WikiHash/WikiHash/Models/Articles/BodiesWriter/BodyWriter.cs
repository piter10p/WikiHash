using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.Bodies;
using System.Xml;

namespace WikiHash.Models.Articles.BodiesWriter
{
    public class BodyWriter
    {
        XmlDocument Doc;
        XmlElement Root;
        Body Body;

        public void Write(Body body, string link)
        {
            try
            {
                if (body == null)
                    throw new ArgumentNullException();

                Body = body;
                CreateDocument();
                GenerateMeta();
                GenerateSections();
                SaveFile(link);
            }
            catch(Exception e)
            {
                throw new Exception("Failed to write body to file.", e);
            }
        }

        private void CreateDocument()
        {
            Doc = new XmlDocument();
            Root = Doc.CreateElement("article");
            Doc.AppendChild(Root);
        }

        private void GenerateMeta()
        {
            try
            {
                var meta = Doc.CreateElement("meta");
                Root.AppendChild(meta);

                var tags = Doc.CreateElement("tags");
                tags.InnerText = Body.MetaData.Tags;
                meta.AppendChild(tags);
            }
            catch(Exception e)
            {
                throw new Exception("Failed to generate meta.", e);
            }
        }

        private void GenerateSections()
        {
            try
            {
                var generator = new SectionGenerator();

                var sections = Doc.CreateElement("sections");
                Root.AppendChild(sections);

                foreach(var section in Body.Sections)
                {
                    var sec = generator.GenerateSection(section, Doc);
                    sections.AppendChild(sec);
                }
            }
            catch(Exception e)
            {
                throw new Exception("Failed to generate sections.", e);
            }
        }

        private void SaveFile(string link)
        {
            try
            {
                var path = PathsGenerator.Article(link);
                Doc.Save(path);
            }
            catch (Exception e)
            {
                throw new Exception("Falied to save article file.", e);
            }
        }
    }
}