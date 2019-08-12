using System;
using System.ComponentModel;
using Xamarin.Forms;

using WallpaperScheduler.Models;
using WallpaperScheduler.ViewModels;

namespace WallpaperScheduler.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            MessagingCenter.Subscribe<Page>(this, "RefreshItemsPage", (sender) => viewModel.LoadItemsCommand.Execute(null));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        private void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var item = (Item)((MenuItem)sender).CommandParameter;

            viewModel.Items.Remove(item);

            MessagingCenter.Send<Page>(this, "RefreshItemsPage");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items?.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}