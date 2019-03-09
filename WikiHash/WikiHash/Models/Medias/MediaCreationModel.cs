using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public class MediaCreationModel
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        [Microsoft.Web.Mvc.FileExtensions(Extensions = "jpg,png,jpeg,gif,apng,svg,bmp,mp4",
             ErrorMessage = "Unsupported file extension. Supported: jpg, png, jpeg, gif, apng, svg, bmp, mp4")]
        public HttpPostedFileBase File { get; set; }
    }
}