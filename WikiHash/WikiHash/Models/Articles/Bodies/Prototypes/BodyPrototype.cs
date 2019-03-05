using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class BodyPrototype
    {
        public MetaData MetaData { get; set; }
        public List<Section> Sections { get; } = new List<Section>();
    }
}