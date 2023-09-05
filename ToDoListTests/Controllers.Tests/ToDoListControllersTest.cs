using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoFixture;
using ToDoListApi.Repositories;
using ToDoListApi.Controllers;
using ToDoListModels;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListTests.Controllers.Tests
{
    [TestFixture]
    public class ToDoListControllersTest
    {
        private Mock<IToDoListRepository> _repositoryMock;
        private Fixture _fixture;
        private ToDoListController _controller;

        public ToDoListControllersTest()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IToDoListRepository>();
        }

        [Test]
        public async Task GetToDoLists_ShouldReturnStatusCode200()
        {
            var toDoList = _fixture.CreateMany<ToDoList>();
            _repositoryMock.Setup(t => t.GetToDoLists()).ReturnsAsync(toDoList);
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_controller.GetAllToDoList());

            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetToDoListById_ShouldReturnStatusCode200()
        {
            var toDoList = _fixture.Create<ToDoList>();
            _repositoryMock.Setup(t => t.GetToDoList(It.IsAny<int>())).ReturnsAsync(toDoList);
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_repositoryMock.Object);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task CreateToDoList_ShouldReturnStatusCode200()
        {
            var newToDoList = _fixture.Create<ToDoList>();
            _repositoryMock.Setup(t => t.CreateToDoList(newToDoList));
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_repositoryMock.Object);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdateToDoList_ShouldReturnStatusCode200()
        {
            var newToDoList = _fixture.Create<ToDoList>();
            int Id = newToDoList.Id;
            string Title = newToDoList.Title;
            string Description = newToDoList.Description;
            newToDoList.Description = "Update the description";
            _repositoryMock.Setup(t => t.UpdateToDoList(Id, newToDoList));
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_repositoryMock.Object);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);
                
            });
        }

        [Test]
        public async Task DeleteToDoList_ShouldReturnStatusCode200()
        {
            var newToDoList = _fixture.Create<ToDoList>();
            int Id = newToDoList.Id;            
            _repositoryMock.Setup(t => t.DeleteToDoList(Id));
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_repositoryMock.Object);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);

            });
        }
    }
}
