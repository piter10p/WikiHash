using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.Bodies;
using Json = Newtonsoft.Json;

namespace WikiHash.Models.Articles
{
    public static class ArticleJsonParser
    {
        public static ArticleViewModel Parse(string jsonText)
        {
            try
            {
                dynamic json = Json.JsonConvert.DeserializeObject(jsonText);
                var title = json.Title;
                var body = ParseBody(json.Body);

                var article = new ArticleViewModel();
                article.Body = body;
                article.Title = title;

                return article;
            }
            catch(Exception e)
            {
                throw new Exception("Failed to parse json Article.", e);
            }
        }

        private static Body ParseBody(dynamic body)
        {
            try
            {
                var sections = body.Sections;
                var metaData = body.MetaData;
                var bodyPrototype = new BodyPrototype();
                bodyPrototype.MetaData = new MetaData((string)metaData.Tags);

                foreach (var sec in sections)
                {
                    var parsedSection = ParseSection(sec);
                    bodyPrototype.Sections.Add(parsedSection);
                }

                return Body.FromPrototype(bodyPrototype);
            }
            catch
            {
                throw;
            }
        }

        private static Section ParseSection(dynamic section)
        {
            try
            {
                var frames = section.ContentFrames;
                var sectionPrototype = new SectionPrototype();
                sectionPrototype.Title = section.Title;

                foreach (var frame in frames)
                {
                    var parsedFrame = ParseFrame(frame);
                    sectionPrototype.ContentFrames.Add(parsedFrame);
                }

                return Section.FromPrototype(sectionPrototype);
            }
            catch
            {
                throw;
            }
        }

        private static ContentFrame ParseFrame(dynamic contentFrame)
        {
            try
            {
                var framePrototype = new ContentFramePrototype();
                framePrototype.Content = contentFrame.Content;
                framePrototype.ContentType = ContentTypeParser.Parse((string)contentFrame.Type);
                framePrototype.Width = FrameWidthParser.Parse((string)contentFrame.Width);

                return ContentFrame.FromPrototype(framePrototype);
            }
            catch
            {
                throw;
            }
        }
    }
}