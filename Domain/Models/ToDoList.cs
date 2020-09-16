using System;
using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Class to hold information about todolist
    /// </summary>
    public class ToDoList : BaseItem
    {
        public List<ToDoItem> TodoItems { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
