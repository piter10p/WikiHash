using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WikiHash.Models
{
    public abstract class Linkable
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(256)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [NotMapped]
        public string Link
        {
            get
            {
                return TitleFunctions.GenerateLink(Title);
            }
        }
    }
}