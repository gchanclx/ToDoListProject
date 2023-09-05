using Microsoft.EntityFrameworkCore;
using System;
using ToDoListApi.Models;
using ToDoListModels;

namespace ToDoListApi.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly DBContextClass _dbContextClass;

        public ToDoListRepository(DBContextClass dbContextClass)
        {
            this._dbContextClass = dbContextClass;
        }

        public async Task<ToDoList> CreateToDoList(ToDoList todolist)
        {
            var result = await _dbContextClass.ToDoList.AddAsync(todolist);
            await _dbContextClass.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ToDoList> DeleteToDoList(int Id)
        {
            var result = await _dbContextClass.ToDoList
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (result != null)
            {
                _dbContextClass.ToDoList.Remove(result);
                await _dbContextClass.SaveChangesAsync();
                return result;
            }

            return null;
        }

        

        public async Task<IEnumerable<ToDoList>> GetToDoLists()
        {
            return await _dbContextClass.ToDoList.ToArrayAsync();
        }

        public async Task<ToDoList> GetToDoList(int ToDoListId)
        {
            return await _dbContextClass.ToDoList
                .FirstOrDefaultAsync(t => t.Id == ToDoListId);
        }

        public async Task<ToDoList> GetToDoList(string title)
        {
            return await _dbContextClass.ToDoList
                .FirstOrDefaultAsync(t => t.Title == title);

        }

        public async Task<ToDoList> UpdateToDoList(int Id, ToDoList todolist)
        {
            var result = await _dbContextClass.ToDoList
                .FirstOrDefaultAsync (t => t.Id == Id);

            if (result != null)
            {
                result.Title = todolist.Title;
                result.Description = todolist.Description;

                await _dbContextClass.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
