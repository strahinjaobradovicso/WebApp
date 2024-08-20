using System.Text.Json;
using WebApp.Models;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    public TimeEntryRepository(HttpClient client, IConfiguration config){
        _client = client;
        _config = config;
    }

    private async Task<IEnumerable<TimeEntryModel>> GetAllTimeEntries()
    {

        List<TimeEntryModel> employees = new();

        HttpResponseMessage Res = await _client.GetAsync(_config["HttpClients:TimeEntries:GetAllUri"]);
        if(Res.IsSuccessStatusCode){
            var employeesResponse = Res.Content.ReadAsStringAsync().Result;
            var parsed = JsonSerializer.Deserialize<List<TimeEntryModel>>(employeesResponse);
            if(parsed != null){
                employees = parsed;
            }
        }

        return employees;

    }


    public async Task<IEnumerable<EmployeeMinutes>> GetEmployeeTotalTime(){
        var res = await GetAllTimeEntries();

        return res.GroupBy(entry => entry.EmployeeName)
        .Select(emp => new EmployeeMinutes {
            Name = emp.Key,
            Total = emp.Aggregate(0, (x, y) => x + (int)(y.EndTimeUtc - y.StarTimeUtc).TotalMinutes)
        });
    }
}