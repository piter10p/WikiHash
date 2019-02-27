using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public static class MediasManager
    {
        public static Media GetMedia(int id)
        {
            var context = DAL.ApplicationDbContext.Create();
            var media = context.Medias.Find(id);

            if (media == null)
                throw new Exception("No media with specified id found.");

            return media;
        }
    }
}