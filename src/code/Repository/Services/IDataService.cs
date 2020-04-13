using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.Repository.Services
{
    public interface IDataService<T>
    {
        Task<int> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
