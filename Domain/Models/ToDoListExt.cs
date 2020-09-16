using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// DTO for todolist
    /// </summary>
    public class ToDoListExt : ToDoItemExt
    {
        public List<ToDoItemExt> ToDoItems { get; set; }
    }
}
