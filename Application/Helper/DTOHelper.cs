using Application.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Helper
{
    /// <summary>
    /// Class to hold methods for converting dto's and vice-versa
    /// </summary>
    public class DTOHelper : IDTO
    {
        public ToDoItem MapItemDTOToAddEntity(BaseToDoItem baseToDo)
        {
            ToDoItem item = new ToDoItem()
            {
                CreatedDate = DateTime.Now,
                LabelId = baseToDo.LabelId,
                Name = baseToDo.Name,
                UserId = baseToDo.UserId,
                IsComplete = baseToDo.IsComplete,

            };
            return item;
        }
        public ToDoItem MapItemDTOToUpdateEntity(BaseToDoItem baseToDo)
        {
            ToDoItem item = new ToDoItem()
            {
                Id = baseToDo.Id,
                UpdatedDate = DateTime.Now,
                LabelId = baseToDo.LabelId,
                Name = baseToDo.Name,
                UserId = baseToDo.UserId,
                IsComplete = baseToDo.IsComplete,

            };
            return item;
        }


        public ToDoList MapListDTOToAddEntity(BaseToDoList baseToDo)
        {
            ToDoList itemList = new ToDoList()
            {
                CreatedDate = DateTime.Now,
                LabelId = baseToDo.LabelId,
                UpdatedDate = (DateTime?)null,
                Name = baseToDo.Name,
                UserId = baseToDo.UserId,
            };
            if (baseToDo.TodoItems != null && baseToDo.TodoItems.Count > 0)
            {
                itemList.TodoItems = new List<ToDoItem>();
                foreach (var item in baseToDo.TodoItems)
                {
                    itemList.TodoItems.Add(MapItemDTOToAddEntity(item));
                }
            }
            return itemList;
        }

        public ToDoList MapListDTOToUpdateEntity(BaseToDoList baseToDo)
        {
            ToDoList itemList = new ToDoList()
            {
                Id = baseToDo.Id,
                UpdatedDate = DateTime.Now,
                LabelId = baseToDo.LabelId,
                Name = baseToDo.Name,
                UserId = baseToDo.UserId,
            };
            if (baseToDo.TodoItems != null && baseToDo.TodoItems.Count > 0)
            {
                itemList.TodoItems = new List<ToDoItem>();
                foreach (var item in baseToDo.TodoItems)
                {
                    itemList.TodoItems.Add(MapItemDTOToUpdateEntity(item));
                }
            }
            return itemList;
        }
    }
}
