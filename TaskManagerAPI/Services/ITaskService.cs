using TaskManagerAPI.Models;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetTasks();
        Task<TaskItemDto?> GetTask(int id);
        Task<TaskItemDto> CreateTask(TaskItem task);
        Task<bool> UpdateTask(int id, TaskItem task);
        Task<bool> DeleteTask(int id);
    }
}
