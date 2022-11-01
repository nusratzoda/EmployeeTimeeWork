namespace Domain.Dtos;

public class GetAttendanceDto
{
    public int Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int EmployeeId { get; set; }
}