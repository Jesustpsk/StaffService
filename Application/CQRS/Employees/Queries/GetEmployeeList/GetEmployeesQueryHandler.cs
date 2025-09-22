using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Employees.Queries.GetEmployeeList;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeVm>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeVm>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        //var employees = _employeeRepository.GetEmployeesAsync()
        
        return new List<EmployeeVm>();
    }
}