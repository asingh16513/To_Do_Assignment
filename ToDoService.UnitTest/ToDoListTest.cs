using Application.Helper;
using Application.Interface;
using Application.ToDoLists.Command.AddToDoList;
using Application.ToDoLists.Command.UpdateCommand;
using Application.ToDoLists.Query.DeleteToDoListQuery;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoService.Controllers;

namespace ToDoService.UnitTest
{
    /// <summary>
    /// Class to do unit test for todolist
    /// </summary>
    public class ToDoListTest
    {
        /// <summary>
        /// Test to add todolist
        /// </summary>
        [Test]
        public void AddToDoListTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            var todoItem = new Domain.Models.BaseToDoItem()
            {
                IsComplete = true,
                LabelId = 1,
                Name = "Item 1",
                UserId = 1
            };
            AddToDoListCommand command = new AddToDoListCommand
            {
                ToDoList = new Domain.Models.BaseToDoList()
                {
                    LabelId = 1,
                    Name = "List 1",
                    TodoItems = new List<Domain.Models.BaseToDoItem>() { todoItem },
                    UserId = 1
                }

            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoListController controller = new ToDoListController(patchToDo.Object, mediator.Object);
            var result = controller.AddToDoList(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }

        /// <summary>
        /// Test to get collection of todolists
        /// </summary>
        [Test]
        public void GetToDoListTest()
        {
            var todoItem = new Domain.Models.ToDoItemExt()
            {
                Id = 2,
                Name = "Item 1",
                Label = "Label 1",
            };
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            List<ToDoListExt> toDoItemExt = new List<ToDoListExt>
            {
                new ToDoListExt()
                {
                    Id = 2,
                    Name = "List 1",
                    Label = "Label 1",
                    ToDoItems = new List<ToDoItemExt>() { todoItem }

                }
            };
            EmptyQuery<List<Domain.Models.ToDoListExt>> command = new EmptyQuery<List<Domain.Models.ToDoListExt>>();
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(toDoItemExt));
            ToDoListController controller = new ToDoListController(patchToDo.Object, mediator.Object);
            var result = controller.GetToDoList(command);
            var response = result.Result as OkObjectResult;
            List<ToDoListExt> items = (List<ToDoListExt>)response.Value;
            Assert.AreEqual(2, items[0].Id);
            Assert.AreEqual("List 1", items[0].Name);

            Assert.AreEqual(2, items[0].ToDoItems[0].Id);
            Assert.AreEqual("Item 1", items[0].ToDoItems[0].Name);
            Assert.AreEqual("Label 1", items[0].ToDoItems[0].Label);
        }

        /// <summary>
        /// Test to udpate todolist
        /// </summary>
        [Test]
        public void UpdateToDoListTest()
        {
            var todoItem = new Domain.Models.BaseToDoItem()
            {
                IsComplete = true,
                LabelId = 1,
                Name = "Item 1",
                UserId = 1
            };
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            UpdateToDoListCommand command = new UpdateToDoListCommand
            {
                ToDoList = new BaseToDoList()
                {
                    LabelId = 2,
                    Name = "Item 3",
                    TodoItems = new List<BaseToDoItem>() { todoItem }
                }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoListController controller = new ToDoListController(patchToDo.Object, mediator.Object);
            var result = controller.UpdateToDoList(1, command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }

        /// <summary>
        /// Test to delete todolist
        /// </summary>
        [Test]
        public void DeleteToDoListTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            DeleteToDoListQuery command = new DeleteToDoListQuery
            {
                ItemId = 1
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoListController controller = new ToDoListController(patchToDo.Object, mediator.Object);
            var result = controller.DeleteToDoList(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }
    }
}
