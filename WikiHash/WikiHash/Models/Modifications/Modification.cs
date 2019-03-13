using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Modifications
{
    public class Modification: Linkable
    {
        [Required]
        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Articles.Article Article { get; set; }

        [Required]
        [MaxLength(15)]
        public string AuthorIp { get; set; }

        //public int
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [NotMapped]
        public override string Link
        {
            get
            {
                if (Article == null)
                    throw new Exception("Article is null. Can not get modification link.");

                return Article.Link + "-" + CreationDate.ToShortDateString() + "-" + String.Format("{0}.{1}.{2}", CreationDate.Hour, CreationDate.Minute, CreationDate.Second);
            }
        }
    }
}