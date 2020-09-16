using Application.Interface;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.QueryTypes
{
    /// <summary>
    /// Queries for GraphQL
    /// </summary>
    [Authorize]
    public class Query
    {
        private readonly IToDoListDbManager _todoListService;
        private readonly IToDoItemDbManager _todoitemService;
        private readonly ILabelDBManager _labelService;
        private readonly IUserManager _userManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query(IToDoItemDbManager service, ILabelDBManager labelService, IToDoListDbManager todoListService, [Service]IUserManager userManager)
        {
            _todoListService = todoListService;
            _todoitemService = service;
            _labelService = labelService;
            _userManager = userManager;
        }

        /// <summary>
        /// Query to get all labels
        /// </summary>
        /// <returns></returns>
        public Task<List<Domain.Models.Label>> GetLabels() => _labelService.GetLabels();

        /// <summary>
        /// Query to get label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public Task<Domain.Models.Label> GetLabelById(int labelId) => _labelService.GetLabelById(labelId);

        /// <summary>
        /// Query to get todolist by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<Domain.Models.BaseToDoList> GetToDoListById(int itemId) => await _todoListService.GetToDoListById(itemId);

        /// <summary>
        /// Query to get all lists
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.Models.ToDoListExt>> GetToDoLists() => await _todoListService.GetToDoList(_userManager.GetUserId());

        /// <summary>
        /// Query to get all todoitems
        /// </summary>
        /// <returns></returns>
        public async Task<List<Domain.Models.ToDoItemExt>> GetToDoItems() => await _todoitemService.GetToDoItems(_userManager.GetUserId());

        /// <summary>
        /// Query to get todoitem by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<Domain.Models.BaseToDoItem> GetToDoItemById(int itemId) => await _todoitemService.GetToDoItem(itemId);

    }








}
