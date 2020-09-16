using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// Interface for db operations for todoitem
    /// </summary>
    public interface IToDoItemDbManager
    {
        Task<int> AddToDoItem(ToDoItem item);
        Task<List<ToDoItemExt>> GetToDoItems(int userId);
        Task<int> UpdateToDoItem(ToDoItem item);
        Task<BaseToDoItem> GetToDoItem(int itemId);
        Task<int> DeleteToDoItem(int itemId);
        Task<List<ToDoItemExt>> SearchToDoItems(int userId, string searchString, int pageNumber, int pageSize);
    }
}
