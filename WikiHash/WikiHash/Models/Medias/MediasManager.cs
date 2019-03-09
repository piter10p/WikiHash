using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Medias
{
    public static class MediasManager
    {
        public static Media GetMedia(string link)
        {
            try
            {
                var context = DAL.ApplicationDbContext.Create();
                var query = from a in context.Medias select a;

                foreach (var media in query)
                {
                    if (media.Link == link)
                        return media;
                }

                throw new Exception("No media with specified link found.");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get media.", e);
            }
        }

        public static void AddMedia(MediaCreationModel creationModel)
        {
            try
            {
                var context = DAL.ApplicationDbContext.Create();
                var media = Media.FromCreationModel(creationModel);

                context.Medias.Add(media);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add media.", e);
            }
        }
    }
}