using ToDoListModels;

namespace ToDoListApi.Repositories
{
    public interface IToDoListRepository
    {
        public Task<IEnumerable<ToDoList>> GetToDoLists();
        public Task<ToDoList> GetToDoList(int Id);
        public Task<ToDoList> GetToDoList(string title);
        public Task<ToDoList> CreateToDoList(ToDoList todolist);
        public Task<ToDoList> UpdateToDoList(int Id, ToDoList todolist);
        public Task<ToDoList> DeleteToDoList(int Id);
    }
}
