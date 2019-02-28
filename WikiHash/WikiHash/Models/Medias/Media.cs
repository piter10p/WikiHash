using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WikiHash.Models.Medias
{
    public sealed class Media: Linkable
    {
        [MaxLength(256)]
        public string Description { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(256)]
        public string FileName { get; set; }

        [NotMapped]
        public string Url
        {
            get
            {
                return "/Content/Medias/" + FileName;
            }
        }

        [NotMapped]
        public string Type
        {
            get
            {
                return MediaTypeGetter.GetMediaType(FileName);
            }
        }
    }
}