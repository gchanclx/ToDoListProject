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

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
            return await _dbContextClass.ToDoTasks.ToListAsync();
        }

        public async Task<ToDoTask> CreateTaskAsync(ToDoTask toDoTask)
        {
            DateTime dt = DateTime.Now;
            toDoTask.CreatedDate = dt;

            var result = await _dbContextClass.ToDoTasks.AddAsync(toDoTask);
            await _dbContextClass.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ToDoTask> DeleteTaskAsync(int Id)
        {
            var result = await _dbContextClass.ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == Id);

            if (result != null)
            {
                _dbContextClass.ToDoTasks.Remove(result);
                await _dbContextClass.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<ToDoTask> GetTaskByIdAsync(int Id)
        {
            return await _dbContextClass.ToDoTasks
                .FirstOrDefaultAsync(t => t.Id == Id);
        }

        public async Task<ToDoTask> GetTaskByDescriptionAsync(string taskDesc)
        {
            var result = await _dbContextClass.ToDoTasks
                .FirstOrDefaultAsync(t => t.TaskDesc == taskDesc);

            return result;

        }

        public async Task<ToDoTask> UpdateTaskAsync(int Id)
        {
            var result = await _dbContextClass.ToDoTasks
                .FirstOrDefaultAsync (t => t.Id == Id);

            if (result != null)
            {
                result.IsCompleted = !result.IsCompleted;

                await _dbContextClass.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
