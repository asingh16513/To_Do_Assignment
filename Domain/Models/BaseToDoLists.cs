using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Base class for todo list
    /// </summary>
    public class BaseToDoList : BaseItem
    {
        public List<BaseToDoItem> TodoItems { get; set; }
    }
}
