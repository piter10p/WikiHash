using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [MaxLength(512)]
        public string ArticleLink { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }
    }
}