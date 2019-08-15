using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallpaperScheduler.Models;
using Xamarin.Essentials;

namespace WallpaperScheduler.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), ImageName = "Day.jpg", Time = new TimeSpan(9, 0, 0) },
                new Item { Id = Guid.NewGuid().ToString(), ImageName = "Evening.jpg", Time = new TimeSpan(17, 0, 0) },
                new Item { Id = Guid.NewGuid().ToString(), ImageName = "Night.jpg", Time = new TimeSpan(21, 0, 0) }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return LoadItems();
            //return await Task.FromResult(items);
        }

        private List<Item> LoadItems()
        {
            var serView = Preferences.Get(Constants.SavedItemsName, "");
            return JsonConvert.DeserializeObject<List<Item>>(serView);
        }
    }
}