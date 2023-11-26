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
    [HttpGet]
    public async Task<IEnumerable<TaskDb>> GefAllTask()
    {
        var tasks = await _context.TaskDb.Include(t => t.Status).ToListAsync();
        return tasks;
    }

    [HttpPost]
    public async Task<TaskDb> addTask(string taskName, string? description, int Id)
    {
        var a = new TaskDb()
        {
            Name = taskName,
            Description = description,
            Status_ID = Id
        };
        _context.TaskDb.Add(a);
        await _context.SaveChangesAsync();
        return a;
    }

    [HttpDelete]
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
    [HttpPut]
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
