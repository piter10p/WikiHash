using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public class MediaViewModel: LinkableViewModel
    {
        public static MediaViewModel FromMedia(Media media)
        {
            var output = new MediaViewModel();
            output.Description = media.Description;
            output.Title = media.Title;
            output.FileName = media.FileName;
            output.Url = media.Url;
            output.Type = media.Type;
            output.CreationDate = media.CreationDate;

            return output;
        }

        [MaxLength(256)]
        public string Description { get; set; }

        [MaxLength(256)]
        public string FileName { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }
    }
}