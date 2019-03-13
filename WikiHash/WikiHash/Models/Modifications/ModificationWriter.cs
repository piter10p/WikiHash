using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiHash.Models.Articles.BodiesWriter;

namespace WikiHash.Models.Modifications
{
    public static class ModificationWriter
    {
        public static void Write(Modification modification, Articles.ArticleViewModel articleViewModel)
        {
            try
            {
                //Save base file
                var writer = new BodyWriter();
                writer.Write(articleViewModel.Body, articleViewModel.Link);

                //Save backup copy
                writer.Write(articleViewModel.Body, modification.Link);
            }
            catch
            {
                throw;
            }
        }
    }
}