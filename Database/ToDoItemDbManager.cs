using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// Class to manage DB operations for ToDoItem
    /// </summary>
    public class ToDoItemDbManager : IToDoItemDbManager
    {
        /// <summary>
        /// Method to add new todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddToDoItem(ToDoItem item)
        {
            using (var context = new ToDoServiceDBContext())
            {
                context.ToDoItems.Add(item);
                int result = await context.SaveChangesAsync();
                return result;
            }
        }

        /// <summary>
        /// Method to get all todoitems by user id. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ToDoItemExt>> GetToDoItems(int userId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                List<ToDoItemExt> items = await (from item in context.ToDoItems
                                                 join label in context.Labels
                                                 on item.LabelId equals label.Id
                                                 where item.UserId == userId
                                                 select new ToDoItemExt
                                                 {
                                                     Id = item.Id,
                                                     Name = item.Name,
                                                     Label = label.Name
                                                 }).ToListAsync();
                return items;
            }
        }

        /// <summary>
        /// Method to get all todoitems based on search criteria and pagesize
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ToDoItemExt>> SearchToDoItems(int userId, string searchString, int pageNumber, int pageSize)
        {
            using (var context = new ToDoServiceDBContext())
            {
                IQueryable<ToDoItemExt> items = (from item in context.ToDoItems
                                                 join label in context.Labels
                                                 on item.LabelId equals label.Id
                                                 where item.UserId == userId
                                                 select new ToDoItemExt
                                                 {
                                                     Id = item.Id,
                                                     Name = item.Name,
                                                     Label = label.Name
                                                 });

                if (!string.IsNullOrEmpty(searchString))
                {
                    return await items.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).DoPaging(pageNumber, pageSize).ToListAsync();
                }
                else
                    return await items.DoPaging(pageNumber, pageSize).ToListAsync();

            }
        }

        /// <summary>
        /// Method to update todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoItem(ToDoItem item)
        {
            using (var context = new ToDoServiceDBContext())
            {
                context.ToDoItems.Attach(item).Property(x => x.CreatedDate).IsModified = false;
                context.ToDoItems.Update(item);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to get todoitem by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<BaseToDoItem> GetToDoItem(int itemId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var item = await context.ToDoItems.FindAsync(itemId);
                return item;
            }
        }

        /// <summary>
        /// Method to delete todoitem by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoItem(int itemId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var item = await context.ToDoItems.FindAsync(itemId);
                context.ToDoItems.Remove(item);
                return await context.SaveChangesAsync();
            }
        }
    }
}
