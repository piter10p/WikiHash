using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Permissions
{
    public enum PermissionTarget
    {
        CreatingNewArticles,
        ModifyingArticlesData,
        ModifyingArticlesBody,
        CreatingNewMedias,
        ReadingArticles
    }
}