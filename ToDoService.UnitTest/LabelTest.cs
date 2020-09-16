using Application.Helper;
using Application.Label.Command.AddLabel;
using Application.Label.Queries.GetLabelById;
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
    /// Class to do unit test for Labels
    /// </summary>
    public class LabelTest
    {
        /// <summary>
        /// Test to add labels
        /// </summary>
        [Test]
        public void AddLabelTest()
        {
            var mediator = new Mock<IMediator>();
            AddLabelCommand command = new AddLabelCommand
            {
                Label = new Label() { Name = "Label2" }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            LabelController controller = new LabelController(mediator.Object);
            var result = controller.AddLabel(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }

        /// <summary>
        /// Test to get all labels
        /// </summary>
        [Test]
        public void GetLabelsTest()
        {
            var mediator = new Mock<IMediator>();
            EmptyQuery<List<Domain.Models.Label>> command = new EmptyQuery<List<Domain.Models.Label>>();
            List<Label> labels = new List<Label>
            {
                new Label() { Id = 1, Name = "Label1" }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(labels));
            LabelController controller = new LabelController(mediator.Object);
            var result = controller.GetLabels(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(labels.Count, ((List<Label>)response.Value).Count);
        }

        /// <summary>
        /// Test to get label by id
        /// </summary>
        [Test]
        public void GetLabelByIdTest()
        {
            var mediator = new Mock<IMediator>();
            GetLabelByIdQuery query = new GetLabelByIdQuery
            {
                LabelId = 1
            };
            Label label = new Label() { Id = 1, Name = "Label1" };
            mediator.Setup(e => e.Send(query, new System.Threading.CancellationToken())).Returns(Task.FromResult(label));
            LabelController controller = new LabelController(mediator.Object);
            var result = controller.GetLabelById(query);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(label.Id, ((Label)response.Value).Id);
            Assert.AreEqual(label.Name, ((Label)response.Value).Name);
        }
    }
}