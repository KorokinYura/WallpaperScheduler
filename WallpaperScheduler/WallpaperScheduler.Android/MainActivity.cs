using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using Android.Content;
using System.IO;

namespace WallpaperScheduler.Droid
{
    [Activity(Label = "Wallpaper Scheduler", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {            
            GetReadStoragePermission();
            CreateWallpaperImagesFolder();






            // It seems that minimum interval is 60000 ms == 1 min
            var interval = 30000;


            var intent = new Intent(ApplicationContext, typeof(BackgroundActivity));
            var pendingIntent = PendingIntent.GetBroadcast(ApplicationContext, 0, intent, 0);

            AlarmManager alarm = (AlarmManager)ApplicationContext.GetSystemService(Context.AlarmService);
            alarm.SetRepeating(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis(), interval, pendingIntent);







            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void GetReadStoragePermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }
        }

        private void CreateWallpaperImagesFolder()
        {
            try
            {
                Configurator.GetImagesNames();
            }
            catch
            {
                // ignore exception in this case
            }
        }
    }
}