using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.Bodies;
using WikiHash.Models.Articles.BodiesWriter;

namespace WikiHash.Models.Articles
{
    public static class NewArticleGenerator
    {
        public static void Create(Article article)
        {
            var writer = new BodyWriter();
            var body = GenerateBody();

            writer.Write(body, article.Link);
        }

        private static Body GenerateBody()
        {
            var contentFrameProto = new ContentFramePrototype();
            contentFrameProto.Content = "{ \"ops\": [ { \"insert\": \"New content farme.\" } ] }";
            contentFrameProto.ContentType = ContentType.Text;
            contentFrameProto.Width = FrameWidth.W6;
            var contentFrame = ContentFrame.FromPrototype(contentFrameProto);

            var sectionProto = new SectionPrototype();
            sectionProto.ContentFrames.Add(contentFrame);
            sectionProto.Title = "New article section";
            var section = Section.FromPrototype(sectionProto);

            var bodyProto = new BodyPrototype();
            bodyProto.Sections.Add(section);
            bodyProto.MetaData = new MetaData("");

            return Body.FromPrototype(bodyProto);
        }
    }
}