using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WallpaperScheduler.Models;
using WallpaperScheduler.Views;
using Newtonsoft.Json;
using System.Linq;
using Xamarin.Essentials;

namespace WallpaperScheduler.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool isInitialLoad = true;


        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            // Save privious state before loading items.
            if (!isInitialLoad)
            {
                SaveCurrentViewState();
            }
            else
            {
                isInitialLoad = false;
            }


            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SaveCurrentViewState()
        {
            try
            {
                var serView = JsonConvert.SerializeObject(Items.ToList());
                Preferences.Set("SavedItems", serView);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}