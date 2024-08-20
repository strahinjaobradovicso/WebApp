using ASPNET_MVC_ChartsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class TimeEntryController : Controller {

    private readonly ITimeEntryRepository _timeEntryReposotory;
    public TimeEntryController(ITimeEntryRepository timeEntryRepository) {
        _timeEntryReposotory = timeEntryRepository;
    }   

    private async Task<IEnumerable<EmployeeHours>> GetTotalHours(){
        var res = await _timeEntryReposotory.GetEmployeeTotalTime();

        return res.Select(emp => new EmployeeHours {
            Name = emp.Name,
            Total = emp.Total/60
        });
    }

    public async Task<IActionResult> Table(){
        var res = await GetTotalHours();
        return View(res.OrderByDescending(emp => emp.Total));
    }

    public async Task<IActionResult> Chart(){
        var res = await GetTotalHours();
        var sum = res.Sum(emp => emp.Total);

        List<DataPoint> dataPoints = res
        .Select(emp => new DataPoint(emp.Name, Math.Round((double)emp.Total / sum * 100)))
        .OrderByDescending(x => x.Y)
        .ToList();

        ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        ViewBag.Title = "Employee Total Time Worked";
        return View();
    }

}