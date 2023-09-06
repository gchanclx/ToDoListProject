using ToDoListModels;

namespace ToDoListApi.Repositories
{
    public interface IToDoListRepository
    {
        public Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        public Task<ToDoTask> GetTaskByIdAsync(int Id);
        public Task<ToDoTask> GetTaskByDescriptionAsync(string taskDesc);
        public Task<ToDoTask> CreateTaskAsync(ToDoTask toDoTask);
        public Task<ToDoTask> UpdateTaskAsync(int Id);
        public Task<ToDoTask> DeleteTaskAsync(int Id);
    }
}
