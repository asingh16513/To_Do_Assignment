using Domain.Models;
using HotChocolate;
using Persistence;
using System.Threading.Tasks;

namespace Application.Mutation
{

    /// <summary>
    /// Class to hold mutations for GraphQL
    /// </summary>
    public class Mutation
    {
        private readonly ILabelDBManager _labelDbService;
        private readonly IToDoListDbManager _todoListService;
        private readonly IToDoItemDbManager _todoitemService;

        public Mutation([Service] ILabelDBManager labelDbService, IToDoItemDbManager service,
           IToDoListDbManager todoListService)
        {
            _todoListService = todoListService;
            _todoitemService = service;
            _labelDbService = labelDbService;
        }

        /// <summary>
        /// Mutation to add label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task<int> AddLabel(Domain.Models.Label label)
        {
            return await _labelDbService.AddLabel(label);
        }

        /// <summary>
        /// Mutation to delete label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<int> DeleteLabelById(int labelId)
        {
            return await _labelDbService.DeleteLabelById(labelId);
        }

        /// <summary>
        /// Mutation to assign label to item
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> AssignLabelToItem(int labelId, int[] itemId)
        {
            return await _labelDbService.AssignLabelToItem(labelId, itemId);
        }

        /// <summary>
        /// Mutation to assign label to list
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> AssignLabelToList(int labelId, int[] itemId)
        {
            return await _labelDbService.AssignLabelToList(labelId, itemId);
        }

        /// <summary>
        /// Mutation to add todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddToDoItem(ToDoItem item)
        {
            return await _todoitemService.AddToDoItem(item);
        }

        /// <summary>
        /// Mutation to delete todoitem
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoItem(int itemId)
        {
            return await _todoitemService.DeleteToDoItem(itemId);
        }

        /// <summary>
        /// Mutation to update todoitem
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoItem(int itemId, ToDoItem item)
        {
            item.Id = itemId;
            return await _todoitemService.UpdateToDoItem(item);
        }

        /// <summary>
        /// Mutation to add todolist
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddToDoList(ToDoList item)
        {
            return await _todoListService.AddToDoList(item);
        }

        /// <summary>
        /// Mutation to delete todolist
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoList(int itemId)
        {
            return await _todoListService.DeleteToDoList(itemId);
        }

        /// <summary>
        /// Mutation to update todolist
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoList(int itemId, ToDoList item)
        {
            item.Id = itemId;
            return await _todoListService.UpdateToDoList(item);
        }
    }

}
