using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class SectionPrototype
    {
        public List<ContentFrame> ContentFrames { get; } = new List<ContentFrame>();
        public string Title { get; set; }
    }
}