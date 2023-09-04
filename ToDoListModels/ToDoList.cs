using System.ComponentModel.DataAnnotations;

namespace ToDoListModels
{
    public class ToDoList
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}