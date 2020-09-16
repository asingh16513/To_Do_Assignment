using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.ToDoLists.Query.SearchToDoList
{
    public class SearchToDoListQuery : IRequest<List<ToDoListExt>>
    {
        public SearchFilter SearchFilter { get; set; }
    }
}
