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
            if (sections == null || metaData == null)
                throw new ArgumentNullException();

            this.sections = sections;
            this.MetaData = metaData;
        }

        public static Body FromPrototype(BodyPrototype bodyPrototype)
        {
            if(bodyPrototype == null)
                throw new ArgumentNullException();

            return new Body(bodyPrototype.Sections, bodyPrototype.MetaData);
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