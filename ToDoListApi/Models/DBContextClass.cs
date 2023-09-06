using Microsoft.EntityFrameworkCore;
using ToDoListModels;

namespace ToDoListApi.Models
{
    public class DBContextClass : DbContext
    {
        
        public DBContextClass(DbContextOptions<DBContextClass> options) 
            : base(options) 
        {
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }
}
