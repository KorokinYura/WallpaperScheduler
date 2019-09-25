using Android.App;
using Android.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WallpaperScheduler.Models;
using Xamarin.Essentials;

namespace WallpaperScheduler.Droid
{
    [BroadcastReceiver]
    public class BackgroundActivity : BroadcastReceiver
    {
        private List<Stream> Images = new List<Stream>();
        private List<string> ImagesNames = new List<string>();

        private List<Item> WallpaperSpans = new List<Item>();


        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                GetImages();

                var timeSpan = WallpaperSpans.Where(ws => DateTime.Now.TimeOfDay.TotalMilliseconds >= ws.Time.TotalMilliseconds)
                    .OrderBy(ws => ws.Time.Milliseconds).LastOrDefault();

                // If current time is less then minimum ws than get latest ws 
                // example (cur - 1 AM, min - 9 AM, max - 21 PM, get - 21 PM)
                if (timeSpan == null)
                {
                    timeSpan = WallpaperSpans.OrderBy(ws => ws.Time.TotalMilliseconds).LastOrDefault();
                }


                if (timeSpan.ImageName == Preferences.Get(Constants.SelectedImageName, ""))
                    return;

                var imageStream = Images[ImagesNames.IndexOf(timeSpan.ImageName)];
                SetWallpaperFromStream(context, imageStream);
                Preferences.Set(Constants.SelectedImageName, timeSpan.ImageName);
            }
            catch (Exception ex)
            {
                // Some unhandled exception that may break the background service
            }
        }

        private void SetWallpaperFromStream(Context context, Stream stream)
        {
            WallpaperManager wallpaperManager
                = WallpaperManager.GetInstance(context);

            wallpaperManager.SetStream(stream);
        }




        private void GetImages()
        {
            Images = new List<Stream>();
            ImagesNames = new List<string>();

            try
            {
                var imgsNames = Configurator.GetImagesNames();

                foreach (var imageName in imgsNames)
                {
                    var stream = File.OpenRead(imageName);
                    Images.Add(stream);
                    ImagesNames.Add(imageName.Substring(imageName.LastIndexOf("/") + 1));
                }



                var serView = Preferences.Get(Constants.SavedItemsName, "");
                var desView = JsonConvert.DeserializeObject<List<Item>>(serView);

                if (desView == null)
                    return;

                WallpaperSpans = desView;
            }
            catch (UnauthorizedAccessException)
            {
                //TODO: Handle the exception when permission was not given
            }
        }
    }
}