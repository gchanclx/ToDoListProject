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
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetAllToDoList()
        {
            try
            {
                return Ok(await _repository.GetToDoLists());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ToDoList>> GetToDoListById(int Id)
        {
            try
            {
                var result = await _repository.GetToDoList(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> CreateToDoList(ToDoList toDoList)
        {
            try
            {
                if (toDoList == null)
                {
                    return BadRequest();
                }

                var todolist = _repository.GetToDoList(toDoList.Title);
                if (todolist != null)
                {
                    ModelState.AddModelError("title", "The title is already in use.");
                    return BadRequest(ModelState);
                }

                var createdToDoList = await _repository.CreateToDoList(toDoList);

                return CreatedAtAction(nameof(GetToDoListById),
                    new { id = createdToDoList.Id }, createdToDoList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new ToDoList.");
            }            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ToDoList>> UpdateToDoList(int Id, ToDoList toDoList)
        {
            try
            {
                if (Id != toDoList.Id)
                {
                    return BadRequest("ID does not match.");
                }

                var toDoListUpdate = await _repository.GetToDoList(Id);

                if (toDoListUpdate == null)
                {
                    return NotFound($"ToDoList with Id = {Id} not found.");
                }

                return await _repository.UpdateToDoList(Id, toDoList);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ToDoList>> DeleteToDoList(int Id)
        {
            try
            {
                var toDoListDetele = await _repository.GetToDoList(Id);

                if (toDoListDetele == null)
                {
                    return NotFound($"ToDoList with Id {Id} not Found.");
                }

                return await _repository.DeleteToDoList(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data.");
            }
        }
    }
}
