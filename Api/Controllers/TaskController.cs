using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<TaskDb> deleteTask(string taskName)
    {
        var objDel = _context.TaskDb.Where(t => t.Name == taskName).FirstOrDefault();
        if (objDel != null)
        {
            _context.TaskDb.Remove(objDel);
            await _context.SaveChangesAsync();
        }
        return objDel;
    }
    [HttpPut("/editTask")]
    public async Task<TaskDb> editTask(string taskName, string newTaskName)
    {

        var objEdit = _context.TaskDb.Where(t => t.Name == taskName).FirstOrDefault();
        if (objEdit != null)
        {
            objEdit.Name = newTaskName;
            await _context.SaveChangesAsync();
        }
        return objEdit;
    }
}
