using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Modifications
{
    public class ModificationViewModel
    {
        public string AuthorIp { get; private set; }
        public string UserEmail { get; private set; }
        public DateTime ModificationDate { get; private set; }
        

        private ModificationViewModel() { }

        public static ModificationViewModel FromModification(Modification modification)
        {
            var result = new ModificationViewModel();
            result.AuthorIp = modification.AuthorIp;
            result.UserEmail = modification.UserEmail;
            result.ModificationDate = modification.CreationDate;

            return result;
        }
    }
}