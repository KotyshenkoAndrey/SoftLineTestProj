using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftLineTestProj.Database;
using SoftLineTestProj.Database.Entities;

namespace SoftLineTestProj.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{ 
    private readonly MyDbContext _context;

    public TaskController(MyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all task
    /// </summary>
    /// <returns> List<TaskDb></returns>
    [HttpGet("/getTask")]
    public async Task<IEnumerable<TaskDb>> GetAllTask()
    {
        var tasks = await _context.TaskDb.Include(t => t.Status).ToListAsync();
        return tasks;
    }
    [HttpGet("/getStatus")]
    public async Task<IEnumerable<Status>> GetAllStatus()
    {
        var tasks = await _context.Status.ToListAsync();
        return tasks;
    }

    [HttpPost("/addTask")]
    public async Task<IActionResult> addTask([FromBody] TaskModel taskModel)
    {
        if (ModelState.IsValid)
        {
            var task = new TaskDb()
            {
                Name = taskModel.taskName,
                Description = taskModel.description,
                Status_ID = taskModel.statusId
            };
            _context.TaskDb.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }
        return BadRequest(ModelState); 
    }

    [HttpDelete("/deleteTask")]
    public async Task<TaskDb> deleteTask([FromBody] TaskModel taskModel)
    {
        var objDel = _context.TaskDb.Where(t => t.Name == taskModel.taskName).FirstOrDefault();
        if (objDel != null)
        {
            _context.TaskDb.Remove(objDel);
            await _context.SaveChangesAsync();
        }
        return objDel;
    }
    [HttpPut("/editTask")]
    public async Task<TaskDb> editTask([FromBody] TaskModelUpdate taskModel)
    {

        var objEdit = _context.TaskDb.Where(t => t.Name == taskModel.CurrentTaskName &&
                                            t.Description == taskModel.CurrentTaskNameDescription &&
                                            t.Status_ID == taskModel.CurrentTaskNameStatusId).FirstOrDefault();
        if (objEdit != null)
        {
            objEdit.Name = taskModel.newTaskName;
            objEdit.Description = taskModel.newDescription;
            objEdit.Status_ID = taskModel.newStatusId;

            await _context.SaveChangesAsync();
        }
        return objEdit;
    }
}
