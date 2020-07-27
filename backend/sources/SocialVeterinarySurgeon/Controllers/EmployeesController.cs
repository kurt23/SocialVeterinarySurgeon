using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialVeterinarySurgeon.BL.Services.Interfaces;
using SocialVeterinarySurgeon.Domain.Entities;
using SocialVeterinarySurgeon.Dto;

namespace SocialVeterinarySurgeon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<EmployeeDto> Upsert(EmployeeDto employeeDto)
        {
            var employee = await _employeeService.Upsert(_mapper.Map<Employee>(employeeDto));
            return _mapper.Map<EmployeeDto>(employee);
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDto> GetById(int id)
        {
            return _mapper.Map<EmployeeDto>(await _employeeService.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employeeService.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetList()
        {
            var employees = await _employeeService.GetList();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        [HttpGet("{id}/pets")]
        public async Task<IEnumerable<PetDto>> GetPets(int id)
        {
            var employees = await _employeeService.GetPets(id);
            return _mapper.Map<IEnumerable<PetDto>>(employees);
        }
    }
}
 