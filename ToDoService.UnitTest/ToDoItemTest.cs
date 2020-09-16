using Application.Helper;
using Application.Interface;
using Application.ToDoItems.Command.AddToDoItem;
using Application.ToDoItems.Command.UpdateCommand;
using Application.ToDoItems.Query.DeleteToDoItemQuery;
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
    /// Class used to do unit test for Todoitem
    /// </summary>
    public class ToDoItemTest
    {
        /// <summary>
        /// Test to add todoitem
        /// </summary>
        [Test]
        public void AddToDoItemTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            AddToDoItemCommand command = new AddToDoItemCommand
            {
                ToDoItem = new Domain.Models.BaseToDoItem()
                {
                    IsComplete = true,
                    LabelId = 1,
                    Name = "Item 1",
                    UserId = 1
                }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoItemsController controller = new ToDoItemsController(patchToDo.Object, mediator.Object);
            var result = controller.AddToDoItem(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }

        /// <summary>
        /// Test to get all todoitems
        /// </summary>
        [Test]
        public void GetToDoItemsTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            List<ToDoItemExt> toDoItemExt = new List<ToDoItemExt>
            {
                new ToDoItemExt()
                {
                    Id = 1,
                    Name = "Item 1",
                    Label = "Label 1"
                },
                new ToDoItemExt()
                {
                    Id = 2,
                    Name = "Item 2",
                    Label = "Label 2"
                }
            };
            EmptyQuery<List<Domain.Models.ToDoItemExt>> command = new EmptyQuery<List<Domain.Models.ToDoItemExt>>();
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(toDoItemExt));
            ToDoItemsController controller = new ToDoItemsController(patchToDo.Object, mediator.Object);
            var result = controller.GetToDoItems(command);
            var response = result.Result as OkObjectResult;
            List<ToDoItemExt> items = (List<ToDoItemExt>)response.Value;
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual("Item 1", items[0].Name);
            Assert.AreEqual("Label 1", items[0].Label);

            Assert.AreEqual(2, items[1].Id);
            Assert.AreEqual("Item 2", items[1].Name);
            Assert.AreEqual("Label 2", items[1].Label);
        }

        /// <summary>
        /// Test to update todoitem
        /// </summary>
        [Test]
        public void UpdateToDoItemTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            UpdateToDoItemCommand command = new UpdateToDoItemCommand
            {
                ToDoItem = new BaseToDoItem()
                {
                    LabelId = 2,
                    Name = "Item 3",
                    IsComplete = false
                }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoItemsController controller = new ToDoItemsController(patchToDo.Object, mediator.Object);
            var result = controller.UpdateToDoItem(1, command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }

        /// <summary>
        /// Test to delete todoitem
        /// </summary>
        [Test]
        public void DeleteToDoItemTest()
        {
            var mediator = new Mock<IMediator>();
            var patchToDo = new Mock<IPatchToDo>();
            DeleteToDoItemQuery command = new DeleteToDoItemQuery
            {
                ItemId = 1
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            ToDoItemsController controller = new ToDoItemsController(patchToDo.Object, mediator.Object);
            var result = controller.DeleteToDoItem(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }
    }
}
