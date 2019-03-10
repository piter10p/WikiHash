using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class MetaData
    {
        public MetaData(string author, string tags)
        {
            Author = author ?? throw new ArgumentNullException();
            Tags = tags ?? throw new ArgumentNullException();
        }

        public string Author { get; private set; }
        public string Tags { get; private set; }
    }
}