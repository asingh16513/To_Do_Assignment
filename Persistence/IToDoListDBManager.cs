using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// Interface for db operations for Todolist
    /// </summary>
    public interface IToDoListDbManager
    {
        Task<int> AddToDoList(ToDoList item);

        Task<List<ToDoListExt>> GetToDoList(int userId);
        Task<BaseToDoList> GetToDoListById(int itemId);

        Task<int> UpdateToDoList(ToDoList item);
        Task<int> DeleteToDoList(int itemId);
        Task<List<ToDoListExt>> SearchToDoList(int userId, string searchString, int pageNumber, int pageSize);
    }
}
