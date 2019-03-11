using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models
{
    public abstract class LinkableViewModel
    {
        [MaxLength(256)]
        public string Title { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string CategoryName { get; set; }

        public string Link
        {
            get
            {
                return TitleFunctions.GenerateLink(Title);
            }
        }
    }
}