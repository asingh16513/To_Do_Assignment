using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    /// <summary>
    /// Class to information about todoitems
    /// </summary>
    [Table("ToDoItems")]
    public class ToDoItem : BaseToDoItem
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? ToDoListId { get; set; }

    }
}
