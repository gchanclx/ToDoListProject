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

            var toDoList = _fixture.CreateMany<ToDoList>(3).ToList();
            
            _dbContext.ToDoList.AddRange(toDoList);
             _dbContext.SaveChanges();

        }

        [Test]
        public void GetToDoLists_ReturnAllToDoList()
        {
            //Arrange
            var sut = new ToDoListRepository(_dbContext);
            var toDoListTest = sut.GetToDoLists();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut, Is.Not.Null);
                Assert.That(toDoListTest.Result.Count(), Is.EqualTo(4));
                Assert.That(toDoListTest.Result.Any(), Is.True);
            });
        }

        [Test]
        public void GetToDoListById_ReturnToDoList()
        {
            var sut = new ToDoListRepository(_dbContext);
            var toDoListById = sut.GetToDoList(It.IsAny<int>());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(sut, Is.Not.Null);
                
            });
        }

        [Test]
        public void CreateToDoList_ReturnCorrectNumberofTask()
        {
            var sut = new ToDoListRepository(_dbContext);
            var newTask = _fixture.Create<ToDoList>();
            var createdTask = sut.CreateToDoList(newTask);
            var toDoLists = sut.GetToDoLists();

            // Assert

            Assert.Multiple(() =>
            {
                Assert.That(sut, Is.Not.Null);
                Assert.That(createdTask.IsCompletedSuccessfully, Is.True);
                Assert.That(toDoLists.Result.Count(), Is.EqualTo(4));
            });
        }

        [Test]
        public void UpdateToDoList_ReturnUpdtedData()
        {
            var sut = new ToDoListRepository( _dbContext);
            var newTask = _fixture.Create<ToDoList>();
            int Id = newTask.Id;
            string Title = newTask.Title;
            string Description = newTask.Description;
            newTask.Description = "Updated the description";

            var UpdatedTask = sut.UpdateToDoList(Id, newTask);

            Assert.Multiple(() =>
            {
                Assert.That(_dbContext, Is.Not.Null);
                Assert.That(newTask, Is.Not.Null);
                Assert.That(newTask.Id, Is.EqualTo(Id));
                Assert.That(newTask.Title, Is.EqualTo(Title));
                Assert.That(newTask.Description, Is.Not.EqualTo(Description));
                Assert.That(newTask.Description, Is.EqualTo("Updated the description"));
            });
        }

        [Test]
        public void DeleteToDoList_ReturnDeleteResult()
        {
            var sut = new ToDoListRepository(_dbContext);
            var newTask = _fixture.Create<ToDoList>();
            int Id = newTask.Id;
            var NumberofTask = sut.GetToDoLists();
            _ = sut.CreateToDoList(newTask);
            var newNumberofTask = sut.GetToDoLists();
            _ = sut.DeleteToDoList(Id);

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
