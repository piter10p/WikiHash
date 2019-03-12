using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public sealed class Article: Linkable
    {
        [NotMapped]
        public Bodies.Body Body
        {
            get
            {
                var reader = new BodiesReader.BodyReader();
                return reader.Read(Link);
            }
        }

        public static Article FromViewModel(ArticleViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var article = new Article();
            article.Title = model.Title;
            article.CreationDate = model.CreationDate;

            return article;
        }

        public static Article FromCreationModel(ArticleCreationModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var article = new Article();
            article.Title = model.Title;
            article.CategoryId = model.CategoryId;
            return article;
        }
    }
}