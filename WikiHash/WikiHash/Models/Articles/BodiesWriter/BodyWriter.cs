using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.Bodies;
using System.Xml;
using System.Xml.Linq;

namespace WikiHash.Models.Articles.BodiesWriter
{
    public class BodyWriter
    {
        XDocument doc;
        XElement root;
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
            doc = new XDocument();
            root = new XElement("article");
            doc.Add(root);
        }

        private void GenerateMeta()
        {
            try
            {
                var meta = new XElement("meta");
                root.Add(meta);

                var author = new XElement("author");
                author.Value = Body.MetaData.Author;
                meta.Add(author);
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
                var sections = new XElement("sections");
                root.Add(sections);

                foreach(var section in Body.Sections)
                {
                    var sec = generator.GenerateSection(section);
                    sections.Add(sec);
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
                var path = ArticlesPathGenerator.Generate(link);
                doc.Save(path);
            }
            catch (Exception e)
            {
                throw new Exception("Falied to save article file.", e);
            }
        }
    }
}