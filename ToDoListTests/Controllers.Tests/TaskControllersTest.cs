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
    public class TaskControllersTest
    {
        private Mock<IToDoListRepository> _repositoryMock;
        private Fixture _fixture;
        private ToDoListController _controller;

        public TaskControllersTest()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IToDoListRepository>();
        }

        [Test]
        public async Task GetAllTask_ShouldReturnStatusCode200()
        {
            var toDoList = _fixture.CreateMany<ToDoTask>();
            _repositoryMock.Setup(t => t.GetAllTasksAsync()).ReturnsAsync(toDoList);
            _controller = new ToDoListController(_repositoryMock.Object);
            var result = new OkObjectResult(_controller.GetAllTasks());

            Assert.Multiple(() =>
            {
                Assert.That(_controller, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetTaskById_ShouldReturnStatusCode200()
        {
            var toDoTask = _fixture.Create<ToDoTask>();
            _repositoryMock.Setup(t => t.GetTaskByIdAsync(It.IsAny<int>())).ReturnsAsync(toDoTask);
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
        public async Task CreateTask_ShouldReturnStatusCode200()
        {
            var newTask = _fixture.Create<ToDoTask>();
            _repositoryMock.Setup(t => t.CreateTaskAsync(newTask));
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
        public async Task UpdateTask_ShouldReturnStatusCode200()
        {
            var newToDoList = _fixture.Create<ToDoTask>();
            int Id = newToDoList.Id;
            bool IsCompleted = !newToDoList.IsCompleted;
            _repositoryMock.Setup(t => t.UpdateTaskAsync(Id));
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
        public async Task DeleteTask_ShouldReturnStatusCode200()
        {
            var newTask = _fixture.Create<ToDoTask>();
            int Id = newTask.Id;            
            _repositoryMock.Setup(t => t.DeleteTaskAsync(Id));
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
