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
                var query = from m in context.Medias select m;

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

        public static MediasListModel GetFilteredMedias(string filter)
        {
            try
            {
                var context = DAL.ApplicationDbContext.Create();
                var query = from m in context.Medias where m.Title.Contains(filter) select m;

                if (query.Count() > Configuration.MediasAJAXListMaximumLength)
                    return MediasListModel.CreateToMuch();

                var mediasViewList = new List<MediaViewModel>();

                foreach (var media in query)
                {
                    var viewModel = MediaViewModel.FromMedia(media);
                    mediasViewList.Add(viewModel);
                }

                return MediasListModel.Create(mediasViewList.ToArray());
            }
            catch(Exception e)
            {
                throw new Exception("Failed to get filtered medias list.", e);
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