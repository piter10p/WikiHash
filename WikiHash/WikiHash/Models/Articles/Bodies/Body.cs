using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class Body
    {
        private List<Section> sections;

        public Body(List<Section> sections, MetaData metaData)
        {
            this.sections = sections;
            this.MetaData = metaData;
        }

        public MetaData MetaData { get; private set; }

        public List<Section> Sections
        {
            get
            {
                var output = new List<Section>();
                foreach (var section in sections)
                    output.Add(section);
                return output;
            }
        }
    }
}