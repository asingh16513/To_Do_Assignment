using Application.User.Command.AuthenticateUser;
using Application.User.Command.RegisterUser;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDoService.Controllers;

namespace ToDoService.UnitTest
{
    public class UserTest
    {
        [Test]
        public void AunthenticateUserTest()
        {
            string authToken = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ0b3B0YWwuY29tIiwiZXhwIjoxNDI2NDIwODAwL
                           CJodHRwOi8vdG9wdGFsLmNvbS9qd3RfY2xhaW1zL2lzX2FkbWluIjp0cnVlLCJjb21wYW55IjoiVG9wdGFsIiwiYXdlc29tZSI6dHJ1ZX0
                            .yRQYnWzskCZUxPwaQupWkiUzKELZ49eM7oWxAQK_ZXw";
            UserAuthResult userAuthResult = new UserAuthResult()
            {
                AuthToken = authToken,
                UserId = 1
            };
            var mediator = new Mock<IMediator>();
            AuthenticateUserCommand command = new AuthenticateUserCommand
            {
                Name = "Admin",
                Password = "admin!123"
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(userAuthResult));
            UserController controller = new UserController(mediator.Object);
            var result = controller.Authenticate(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, ((UserAuthResult)response.Value).UserId);
            Assert.AreEqual(authToken, ((UserAuthResult)response.Value).AuthToken);
        }

        [Test]
        public void RegisterUserTest()
        {
            var mediator = new Mock<IMediator>();
            RegisterUserCommand command = new RegisterUserCommand()
            {
                User = new User()
                {
                    Name = "Admin",
                    Password = "admin!123"
                }
            };
            mediator.Setup(e => e.Send(command, new System.Threading.CancellationToken())).Returns(Task.FromResult(1));
            UserController controller = new UserController(mediator.Object);
            var result = controller.RegisterUser(command);
            var response = result.Result as OkObjectResult;
            Assert.AreEqual(1, (int)response.Value);
        }
    }
}
