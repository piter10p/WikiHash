using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class Section
    {
        private List<ContentFrame> contentFrames;

        public Section(string title, List<ContentFrame> frames)
        {
            if (title == null || frames == null)
                throw new ArgumentNullException();

            Title = title;
            contentFrames = frames;
        }

        public static Section FromPrototype(SectionPrototype prototype)
        {
            if (prototype == null)
                throw new ArgumentNullException();

            return new Section(prototype.Title, prototype.ContentFrames);
        }

        public string Title { get; private set; }
        public List<ContentFrame> Frames
        {
            get
            {
                var output = new List<ContentFrame>();
                foreach (var frame in contentFrames)
                    output.Add(frame);
                return output;
            }
        }
    }
}