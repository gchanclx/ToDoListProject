using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListModels
{
    public class ToDoTask
    {
        public int Id { get; set; }
        [Required]
        public string? TaskDesc { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
