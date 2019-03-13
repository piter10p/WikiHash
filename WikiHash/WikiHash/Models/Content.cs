using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models
{
    public abstract class Content: Linkable
    {
        [Index(IsUnique = true)]
        [MaxLength(256)]
        public string Title { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public override string Link
        {
            get
            {
                return TitleFunctions.GenerateLink(Title);
            }
        }
    }
}