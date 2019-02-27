using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public sealed class Article: Linkable
    {
        [NotMapped]
        public Bodies.Body Body
        {
            get
            {
                var reader = new BodiesReader.BodyReader();
                return reader.Read(Link);
            }
        }
    }
}