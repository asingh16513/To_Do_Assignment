using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// Class to manage DB operation for Labels
    /// </summary>
    public class LabelDbManager : ILabelDBManager
    {
        /// <summary>
        /// Method to add label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task<int> AddLabel(Domain.Models.Label label)
        {
            using (var context = new ToDoServiceDBContext())
            {
                context.Labels.Add(label);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to delete label by id.
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<int> DeleteLabelById(int labelId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var id = await context.Labels.FindAsync(labelId);
                context.Labels.Remove(id);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to get label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<Label> GetLabelById(int labelId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var medicine = await context.Labels.FirstOrDefaultAsync(p => p.Id == labelId);
                return medicine;
            }
        }

        /// <summary>
        /// Method to get colllection of labels
        /// </summary>
        /// <returns></returns>
        public async Task<List<Label>> GetLabels()
        {
            using (var context = new ToDoServiceDBContext())
            {
                var labels = await context.Labels.ToListAsync();
                return labels;
            }
        }

        /// <summary>
        /// Method to assign label to todoitem
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> AssignLabelToItem(int labelId, int[] itemId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var items = context.ToDoItems;
                foreach (BaseToDoItem item in items)
                {
                    if (itemId.Contains(item.Id))
                    {
                        item.LabelId = labelId;
                    }
                }
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to assign label to todolist
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<int> AssignLabelToList(int labelId, int[] listId)
        {
            using (var context = new ToDoServiceDBContext())
            {
                var items = context.ToDoLists;
                foreach (ToDoList list in items)
                {
                    if (listId.Contains(list.Id))
                    {
                        list.LabelId = labelId;
                    }
                }
                return await context.SaveChangesAsync();
            }
        }
    }
}
