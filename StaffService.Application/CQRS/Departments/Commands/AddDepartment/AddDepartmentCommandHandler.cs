using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Interfaces;

namespace StaffService.Application.CQRS.Departments.Commands.AddDepartment;

public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, int>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public AddDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _mapper.Map<Department>(request);
        
        var result = await _departmentRepository.AddAsync(department);
        
        return result;
    }
}