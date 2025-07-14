using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere token JWT válido para acceder
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    // Obtiene las tareas solo del usuario autenticado
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return Ok(tasks);
    }

    // POST: api/tasks
    // Crear una nueva tarea para el usuario autenticado
    [HttpPost]
    public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] TaskDTO taskDTO)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var task = new TodoTask
        {
            Title = taskDTO.Title,
            Completed = taskDTO.Completed,
            UserId = userId,

        };
      

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    // PUT: api/tasks/{id}
    // Actualizar una tarea existente del usuario autenticado
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO taskDTO)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            return NotFound();

        task.Title = taskDTO.Title;
        task.Completed = taskDTO.Completed;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/tasks/{id}
    // Eliminar una tarea del usuario autenticado
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
