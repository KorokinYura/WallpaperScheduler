using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WallpaperScheduler.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        //Task<bool> UpdateItemsAsync(List<T> items);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
