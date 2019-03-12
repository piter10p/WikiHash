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
                if (link == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();
                var query = from m in context.Medias.Include("Category") select m;

                foreach (var media in query)
                {
                    if (media.Link == link)
                        return media;
                }

                throw new KeyNotFoundException();
            }
            catch
            {
                throw;
            }
        }

        public static MediasListModel GetFilteredMedias(string filter)
        {
            try
            {
                if (filter == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();
                var query = from m in context.Medias.Include("Category") where m.Title.Contains(filter) || m.Category.CategoryName.Contains(filter) select m;

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
            catch
            {
                throw;
            }
        }

        public static void AddMedia(MediaCreationModel creationModel)
        {
            try
            {
                var context = DAL.ApplicationDbContext.Create();
                var media = Media.FromCreationModel(creationModel);

                var query = from m in context.Medias where m.Title == media.Title select m;

                if (query.Count() != 0)
                    throw new EntryExistsException();

                context.Medias.Add(media);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}