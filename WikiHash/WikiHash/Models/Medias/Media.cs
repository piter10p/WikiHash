using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        [MaxLength(128)]
        public string Link { get; set; }
    }
}