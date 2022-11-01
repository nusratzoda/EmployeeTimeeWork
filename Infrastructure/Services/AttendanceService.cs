using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Response;
using Infrastructure.Context;
namespace Infrastructure.Services;
public class AttendanceService : IAttendanceService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public AttendanceService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> AddAttendance(AddAttendanceDto model)
    {
        try
        {
            var mapped = _mapper.Map<Attendance>(model);
            await _context.Attendances.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>(_mapper.Map<string>("Attendance Added Successfully"));
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetEmployeeAverageAttendace>> GetEmployeeAttendanceAvearageById(EmployeeAttendaceFilterDto filter)
    {
        var employee = await _context.Employees.FindAsync(filter.EmployeeId);
                if (employee == null) return new Response<GetEmployeeAverageAttendace>(System.Net.HttpStatusCode.NotFound, "not found");

        var result = _context.Attendances.
        Where(x => x.EmployeeId == filter.EmployeeId &&
            x.Date >= filter.StartDate.UtcDateTime &&
            x.Date < filter.EndDate.UtcDateTime).ToList();
        if (result.Count == 0) return new Response<GetEmployeeAverageAttendace>(System.Net.HttpStatusCode.NotFound, "not found");
        var average = result.Average(x => x.StartTime.TotalMilliseconds);
        var response = new GetEmployeeAverageAttendace
        {
            AverageTime = TimeSpan.FromMilliseconds(average).ToString(),
            EmployeeId = filter.EmployeeId
        };
        return new Response<GetEmployeeAverageAttendace>(response);
    }
}