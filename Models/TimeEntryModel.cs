namespace WebApp.Models;

public class TimeEntryModel {

    public string Id { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;

    public DateTime StarTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }

    public string EntryNotes { get; set; } = string.Empty;

    public DateTime? DeletedOn { get; set; }
}