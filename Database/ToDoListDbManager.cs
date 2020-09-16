using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// Class to manage DB operations for Todolist
    /// </summary>
    public class ToDoListDbManager : IToDoListDbManager
    {
        /// <summary>
        /// Method to add new todolist
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddToDoList(ToDoList item)
        {
            int result = 0;
            using (var context = new ToDoServiceDBContext())
            {
                context.ToDoLists.Add(item);
                result = await context.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>
        /// Method to get collection for todolist by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ToDoListExt>> GetToDoList(int userId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                List<ToDoListExt> items = await (from item in context.ToDoLists
                                                 join label in context.Labels
                                                 on item.LabelId equals label.Id
                                                 where item.UserId == userId
                                                 select new ToDoListExt
                                                 {
                                                     Id = item.Id,
                                                     Name = item.Name,
                                                     Label = label.Name
                                                 }).ToListAsync();

                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.ToDoItems = new List<ToDoItemExt>();
                        var db_item = await (from to_do_item in context.ToDoItems
                                             join label in context.Labels
                                             on to_do_item.LabelId equals label.Id
                                             where to_do_item.ToDoListId != null && to_do_item.ToDoListId.Value == item.Id
                                             select new ToDoItemExt
                                             {
                                                 Id = to_do_item.Id,
                                                 Name = to_do_item.Name,
                                                 Label = label.Name
                                             }).ToListAsync();

                        item.ToDoItems.AddRange(db_item);
                    }

                }
                return items;
            }
        }

        /// <summary>
        /// Method to get collection of todolist based on search criteria and pagesize
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ToDoListExt>> SearchToDoList(int userId, string searchString, int pageNumber, int pageSize)
        {
            List<ToDoListExt> toDoLists = null;
            using (var context = new ToDoServiceDBContext())
            {
                IQueryable<ToDoListExt> items = (from item in context.ToDoLists
                                                 join label in context.Labels
                                                 on item.LabelId equals label.Id
                                                 where item.UserId == userId
                                                 select new ToDoListExt
                                                 {
                                                     Id = item.Id,
                                                     Name = item.Name,
                                                     Label = label.Name
                                                 });

                if (!string.IsNullOrEmpty(searchString))
                {
                    toDoLists = await items.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).DoPaging(pageNumber, pageSize).ToListAsync();
                }
                else
                    toDoLists = await items.DoPaging(pageNumber, pageSize).ToListAsync();


                if (toDoLists != null && toDoLists.Count > 0)
                {
                    foreach (var item in toDoLists)
                    {
                        item.ToDoItems = new List<ToDoItemExt>();
                        var db_item = await (from to_do_item in context.ToDoItems
                                             join label in context.Labels
                                             on to_do_item.LabelId equals label.Id
                                             where to_do_item.ToDoListId != null && to_do_item.ToDoListId.Value == item.Id
                                             select new ToDoItemExt
                                             {
                                                 Id = to_do_item.Id,
                                                 Name = to_do_item.Name,
                                                 Label = label.Name
                                             }).ToListAsync();

                        item.ToDoItems.AddRange(db_item);
                    }

                }
                return toDoLists;
            }
        }

        /// <summary>
        /// Mehtod to update todolist by id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoList(ToDoList item)
        {
            int result = 0;
            using (var context = new ToDoServiceDBContext())
            {
                context.ToDoLists.Update(item);
                result = await context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<BaseToDoList> GetToDoListById(int itemId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                BaseToDoList baseItem = (from item in context.ToDoLists
                                         join label in context.Labels
                                         on item.LabelId equals label.Id
                                         where item.Id == itemId
                                         select new BaseToDoList
                                         {
                                             Id = item.Id,
                                             Name = item.Name,
                                         }).FirstOrDefault();

                if (baseItem != null)
                {
                    baseItem.TodoItems = new List<BaseToDoItem>();
                    var db_item = await (from to_do_item in context.ToDoItems
                                         join label in context.Labels
                                         on to_do_item.LabelId equals label.Id
                                         where to_do_item.ToDoListId != null && to_do_item.ToDoListId.Value == baseItem.Id
                                         select new BaseToDoItem
                                         {
                                             Id = to_do_item.Id,
                                             Name = to_do_item.Name,
                                         }).ToListAsync();

                    baseItem.TodoItems.AddRange(db_item);
                }
                return baseItem;
            }
        }


        /// <summary>
        /// Method to delete todolist by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoList(int itemId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var item = await context.ToDoLists.FindAsync(itemId);
                context.ToDoLists.Remove(item);
                return await context.SaveChangesAsync();
            }
        }

    }
}
