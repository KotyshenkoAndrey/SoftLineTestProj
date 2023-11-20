using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftLineTestProj.Database;
using SoftLineTestProj.Database.Entities;
using WebApplication2;

namespace SoftLineTestProj.Controllers;

//[ApiController]
//[Route("[controller]")]
//public class WeatherForecastController : ControllerBase
//{
//    private static readonly string[] Summaries = new[]
//    {
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//    private readonly ILogger<WeatherForecastController> _logger;

//    public WeatherForecastController(ILogger<WeatherForecastController> logger)
//    {
//        _logger = logger;
//    }

//[HttpGet]
//public IEnumerable<WeatherForecast> Get()
//{
//    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//    {
//        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//        TemperatureC = Random.Shared.Next(-20, 55),
//        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//    })
//    .ToArray();
//}
//    }

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
    public List<TaskDb> GefAllTask()
    {
        var tasks = _context.TaskDb.Include(t => t.Status).ToList();
        return tasks;
    }

    [HttpPost]
    public void addTask(string taskName, string? description, int Id)
    {
        var a = new TaskDb()
        {
            Name = taskName,
            Description = description,
            Status_ID = Id
        };
        _context.TaskDb.Add(a);
        _context.SaveChanges();
    }

    [HttpDelete]
    public void deleteTask(string taskName)
    {
        var objDel = _context.TaskDb.Where(t => t.Name == taskName).FirstOrDefault();
        if (objDel != null)
        {
            _context.TaskDb.Remove(objDel);
            _context.SaveChanges();
        }       
    }
    //[HttpPut]
    //public void editTask(string taskName, string newTaskName)
    //{

    //    var objEdit = _context.TaskDb.Where(t => t.Name == taskName).FirstOrDefault();
    //    if(objEdit != null)
    //    {
    //        objEdit.Name = newTaskName;
    //    }
    //    _context.Update(objEdit);
    //    _context.SaveChanges();
    //}
}
