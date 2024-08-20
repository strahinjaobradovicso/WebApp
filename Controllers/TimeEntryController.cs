using Microsoft.AspNetCore.Mvc;
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

}