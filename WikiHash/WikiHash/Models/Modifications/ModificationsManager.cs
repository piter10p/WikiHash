using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Modifications
{
    public static class ModificationsManager
    {
        public static Modification AddModification(Modification modification)
        {
            try
            {
                if (modification == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();

                var query = from m in context.Modifications
                            where m.CreationDate == modification.CreationDate && m.Article.Title == modification.Article.Title
                            select m;

                if (query.Count() != 0)
                    throw new EntryExistsException();

                var result = context.Modifications.Add(modification);
                context.Entry(result.Article).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}