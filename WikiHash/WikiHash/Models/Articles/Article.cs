using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [MaxLength(512)]
        public string Link { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        [NotMapped]
        public Bodies.Body Body
        {
            get
            {
                var reader = new BodiesReader.BodyReader();
                return reader.Read(Link);
            }
        }
    }
}