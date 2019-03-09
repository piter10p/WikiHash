using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public class MediasListModel
    {
        private MediasListModel(bool toMuchResults, MediaViewModel[] medias)
        {
            if (medias == null)
                throw new ArgumentNullException();

            ToMuchResults = toMuchResults;
            Medias = medias;
        }

        public bool ToMuchResults { get; private set; }
        public MediaViewModel[] Medias { get; private set; }

        public static MediasListModel CreateToMuch()
        {
            return new MediasListModel(true, new MediaViewModel[] { });
        }

        public static MediasListModel Create(MediaViewModel[] medias)
        {
            if(medias == null)
                throw new ArgumentNullException();

            return new MediasListModel(false, medias);
        }
    }
}