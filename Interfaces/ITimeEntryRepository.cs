using WebApp.Models;

public interface ITimeEntryRepository {
    Task<IEnumerable<EmployeeMinutes>> GetEmployeeTotalTime();
}