using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.ToDoItems.Query.SearchToDoItem
{
    public class SearchToDoItemQuery : IRequest<List<ToDoItemExt>>
    {
        public SearchFilter SearchFilter { get; set; }
    }
}
