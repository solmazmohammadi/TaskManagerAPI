using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>()
        {
        new TaskItem { Id = 1, Title = "Learn C#", IsCompleted = false },
        new TaskItem { Id = 2, Title = "Build a Web API", IsCompleted = false },
        new TaskItem { Id = 3, Title = "Deploy it someday", IsCompleted = false },

        };

        [HttpGet]
        public IEnumerable<TaskItem> Get()
        { 
            return tasks;
        }

        [HttpPost]
        public ActionResult<TaskItem> Post(TaskItem newTask)
        {
            newTask.Id = tasks.Count + 1;
            tasks.Add(newTask);
            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id) 
        {
            var task = tasks.FirstOrDefault(task => task.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TaskItem updatedTask)
        {
            var existingTask = tasks.FirstOrDefault(t =>t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }
            existingTask.Title = updatedTask.Title;
            existingTask.IsCompleted = updatedTask.IsCompleted;
            return NoContent();
        }
    }

    
}
