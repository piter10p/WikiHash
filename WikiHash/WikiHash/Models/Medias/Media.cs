using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WikiHash.Models.Medias
{
    public sealed class Media: Content
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

        public static Media FromCreationModel(MediaCreationModel model)
        {
            var output = new Media();
            output.Title = model.Title;
            output.CreationDate = DateTime.Now;
            output.Description = model.Description;
            output.FileName = model.File.FileName;
            output.CategoryId = model.CategoryId;

            return output;
        }
    }
}