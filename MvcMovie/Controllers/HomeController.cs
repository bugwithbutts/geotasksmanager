using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            return View(await db.GeoTasks.ToListAsync());
        }
        public IActionResult AddGeoTask()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGeoTask(GeoTask task)
        {
            db.GeoTasks.Add(task);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }        
        public async Task<IActionResult> DeleteGeoTask(int? id)
        {
            if(id!=null)
            {
                GeoTask? task = await db.GeoTasks.FirstOrDefaultAsync(p=>p.Id==id);
                if (task != null)
                {
                    db.Remove(task);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> ChooseGroup(int? id)
        {
            if(id!=null)
            {
                GeoTask? task = await db.GeoTasks.FirstOrDefaultAsync(p=>p.Id==id);
                if (task != null) return View(task);
            }
            return NotFound();
        }
        public async Task<IActionResult> FinishGeoTask(int? id)
        {
            if(id!=null)
            {
                GeoTask? task = await db.GeoTasks.FirstOrDefaultAsync(p=>p.Id==id);
                if (task != null && task.Status != "Завершено") return View(task);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> FinishGeoTask(GeoTask newTask, int? id)
        {
            if(id!=null)
            {
                GeoTask? task = await db.GeoTasks.FirstOrDefaultAsync(p=>p.Id==id);
                if (task != null && task.Group != 0 && newTask.DayEnd >= task.DayBegin)
                {
                    task.Status = "Завершено";
                    task.Note = newTask.Note;
                    task.DayEnd = newTask.DayEnd;
                    await db.SaveChangesAsync();
                }
            }                        
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ChooseGroup(GeoTask newTask, int? id)
        {
            if(id!=null)
            {
                GeoTask? task = await db.GeoTasks.FirstOrDefaultAsync(p=>p.Id==id);
                if (task != null)
                {
                    task.Status = "Выполняется";
                    task.Group = newTask.Group;
                    await db.SaveChangesAsync();
                }

            }                        
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MonthInfo(GeoTask task)
        {
            return View(await db.GeoTasks                                    
                                .Where(p => p.Group == task.Group 
                                    && p.Status == "Завершено" 
                                    && DateTime.Now.Month == p.DayEnd.Month)
                                .ToListAsync());
        }

        public IActionResult GetMonthInfo()
        {
            return View();
        }
    }
}