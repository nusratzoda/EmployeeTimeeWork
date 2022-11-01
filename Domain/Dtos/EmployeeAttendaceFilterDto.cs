namespace Domain.Dtos;

public class EmployeeAttendaceFilterDto
{
   public int EmployeeId { get; set; }
   public DateTimeOffset StartDate { get; set; }
   public DateTimeOffset EndDate  { get; set; }
}
