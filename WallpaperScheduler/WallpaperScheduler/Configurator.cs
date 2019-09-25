using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WallpaperScheduler
{
    public static class Configurator
    {
        public static string[] GetImagesNames()
        {
            try
            {
                if (!Directory.Exists(Constants.ImagesPath))
                {
                    Directory.CreateDirectory(Constants.ImagesPath);
                }

                return Directory.GetFiles(Constants.ImagesPath);

            }
            catch (Exception ex)
            {
                // ignore exception in this case
            }

            return new string[0];
        }
    }
}
