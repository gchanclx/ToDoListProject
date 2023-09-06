using Microsoft.AspNetCore.Components;
using ToDoListModels;

namespace ToDoListApp.Pages
{
    public class ToDoListingBase : ComponentBase
    {
        public IEnumerable<ToDoTask> ToDoLists { get; set; }
    }
}
