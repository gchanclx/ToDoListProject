using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ToDoListApi.Repositories;
using ToDoListModels;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _repository;

        public ToDoListController(IToDoListRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> GetAllTasks()
        {
            try
            {
                return (await _repository.GetAllTasksAsync()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ToDoTask>> GetTaskById(int Id)
        {
            try
            {
                var result = await _repository.GetTaskByIdAsync(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoTask>> CreateTask(ToDoTask toDoTask)
        {
            try
            {
                if (toDoTask == null)
                {
                    return BadRequest();
                }
                
                var newTask = await _repository.GetTaskByDescriptionAsync(toDoTask.TaskDesc);

                if (newTask != null)
                {
                    ModelState.AddModelError("Task", "The Task is already existing.");
                    return BadRequest(ModelState);
                }
                
                var createdTask = await _repository.CreateTaskAsync(toDoTask);

                return CreatedAtAction(nameof(GetTaskById),
                    new { id = createdTask.Id }, createdTask);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Task.");
            }            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ToDoTask>> UpdateTask(int Id)
        {
            try
            {
                var toDoListUpdate = await _repository.GetTaskByIdAsync(Id);

                if (toDoListUpdate == null)
                {
                    return NotFound($"Task Id: {Id} not found.");
                }

                return await _repository.UpdateTaskAsync(Id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ToDoTask>> DeleteTask(int Id)
        {
            try
            {
                var toDoListDetele = await _repository.GetTaskByIdAsync(Id);

                if (toDoListDetele == null)
                {
                    return NotFound($"Task Id: {Id} not Found.");
                }

                return await _repository.DeleteTaskAsync(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data.");
            }
        }
    }
}
