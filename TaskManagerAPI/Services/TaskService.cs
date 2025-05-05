using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dtos;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskService
    {
        private readonly TaskContext _context;

        public TaskService(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItemDto>> GetTasks()
        {
            return await _context.Tasks
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title
                })
                .ToListAsync();
        }
        
        public async Task<TaskItemDto?> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return null;
            }

            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title
            };
        }

        public async Task<TaskItemDto> CreateTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title
            };
        }  
        
        public async Task<bool> UpdateTask(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return false;
            }
            _context.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskExists(id))
                {
                    return false;
                }
                throw;
            }
            return true;
        }
        public async Task<bool> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task<bool> TaskExists(int id)
        {
            return await _context.Tasks.AnyAsync(e => e.Id == id);
        }
    }
}
