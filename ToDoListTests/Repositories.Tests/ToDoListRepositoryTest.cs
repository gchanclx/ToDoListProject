using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApi.Repositories;
using Moq;
using AutoFixture;
using ToDoListApi.Models;
using ToDoListModels;
using NuGet.Frameworks;

namespace ToDoListTests.Repositories.Tests
{
    [TestFixture]
    public class ToDoListRepositoryTest : IDisposable
    {
        protected readonly DBContextClass _dbContext;
        private Fixture _fixture;


        public ToDoListRepositoryTest()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<DBContextClass>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new DBContextClass(options);
            _dbContext.Database.EnsureCreated();

            var toDoList = _fixture.CreateMany<ToDoTask>(3).ToList();
            
            _dbContext.ToDoTasks.AddRange(toDoList);
             _dbContext.SaveChanges();

        }

        [Test]
        public void GetAllTasks_ReturnAllTasks()
        {
            //Arrange
            var sut = new ToDoListRepository(_dbContext);
            var toDoListTest = sut.GetAllTasksAsync();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(toDoListTest.Result.Count(), Is.GreaterThan(0));
                Assert.That(toDoListTest.Result.Any(), Is.True);
            });
        }

        [Test]
        public void GetTaskById_ReturnTask()
        {
            var sut = new ToDoListRepository(_dbContext);
            var taskById = sut.GetTaskByIdAsync(It.IsAny<int>());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
            });
        }

        [Test]
        public void CreateTask_ReturnNewData()
        {
            var sut = new ToDoListRepository(_dbContext);
            var newTask = _fixture.Create<ToDoTask>();
            var createdTask = sut.CreateTaskAsync(newTask);
            var toDoLists = sut.GetAllTasksAsync();

            // Assert

            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(sut, Is.Not.Null);
                Assert.That(createdTask.IsCompletedSuccessfully, Is.True);
                Assert.That(toDoLists.Result.Count(), Is.EqualTo(4));
            });
        }

        [Test]
        public void UpdateTask_ReturnUpdtedData()
        {
            var sut = new ToDoListRepository( _dbContext);
            var newTask = _fixture.Create<ToDoTask>();
            int Id = newTask.Id;
            bool IsCompleted = !newTask.IsCompleted;

            var UpdatedTask = sut.UpdateTaskAsync(Id);

            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(newTask, Is.Not.Null);
                Assert.That(newTask.Id, Is.EqualTo(Id));
                Assert.That(newTask.IsCompleted, Is.Not.EqualTo(IsCompleted));
            });
        }

        [Test]
        public void DeleteTask_ReturnDeleteData()
        {
            var sut = new ToDoListRepository(_dbContext);
            var newTask = _fixture.Create<ToDoTask>();
            int Id = newTask.Id;
            var NumberofTask = sut.GetAllTasksAsync();
            _ = sut.CreateTaskAsync(newTask);
            var newNumberofTask = sut.GetAllTasksAsync();
            _ = sut.DeleteTaskAsync(Id);

            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(newTask, Is.Not.Null);
                Assert.That(newTask.Id, Is.EqualTo(Id));
                Assert.That(newNumberofTask, Is.Not.EqualTo(NumberofTask));

            });

        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }


}
