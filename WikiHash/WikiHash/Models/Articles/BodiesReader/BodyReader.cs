using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using WikiHash.Models.Articles.Bodies;

namespace WikiHash.Models.Articles.BodiesReader
{
    public class BodyReader
    {
        private XmlDocument doc;
        private XmlNode sectionsNode;
        private XmlNode metaNode;

        public Body Read(string link)
        {
            try
            {
                LoadFile(link);
                ReadDocumentStructure();
                return ReadData();
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read article body.", e);
            }
        }

        private void LoadFile(string link)
        {
            try
            {
                var path = ArticlesPathGenerator.Generate(link);

                var fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                    throw new Exception("File not exists.");

                doc = new XmlDocument();
                doc.Load(path);
            }
            catch(Exception e)
            {
                throw new Exception("Falied to load article file.", e);
            }
        }

        private void ReadDocumentStructure()
        {
            try
            {
                var root = doc.FirstChild;
                metaNode = root.SelectSingleNode("meta");
                sectionsNode = root.SelectSingleNode("sections");
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read document structure. File is propably corrupted.", e);
            }
        }

        private Body ReadData()
        {
            try
            {
                var meta = MetaDataReader.Read(metaNode);
                var sections = SectionReader.Read(sectionsNode);
                return new Body(sections, meta);
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read article data.", e);
            }
        }
    }
}