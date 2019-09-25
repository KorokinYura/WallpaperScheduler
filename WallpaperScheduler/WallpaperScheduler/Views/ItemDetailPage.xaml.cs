using System.ComponentModel;
using Xamarin.Forms;
using WallpaperScheduler.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace WallpaperScheduler.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        public Item Item { get; }
        public List<string> ImagesNames { get; private set; }

        public ItemDetailPage(Item item)
        {
            Item = item;
            ImagesNames = Configurator.GetImagesNames().Select(n => Path.GetFileName(n)).ToList();

            InitializeComponent();

            BindingContext = this;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<Page>(this, "RefreshItemsPage");
        }
    }
}