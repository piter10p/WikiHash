using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class MetaData
    {
        public MetaData(string tags)
        {
            Tags = tags ?? throw new ArgumentNullException();
        }

        public string Tags { get; private set; }
    }
}