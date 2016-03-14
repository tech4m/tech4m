using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech4mUI.Extensions
{
    public static class FileUploadExtension
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }
    }
}