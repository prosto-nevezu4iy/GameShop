using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace WebUI.Validators
{
    public class ImagesValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var files = value as IEnumerable<HttpPostedFileBase>;

            if (files.ToList()[0] == null)
            {
                return true;
            }

            foreach(var file in files)
            {
                if (file.ContentLength > 2 * 1024 * 1024)
                {
                    return false;
                }

                try
                {
                    using (var img = Image.FromStream(file.InputStream))
                    {
                        return img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg);
                    }
                }
                catch { }
            }
            
            return false;
        }
    }
}