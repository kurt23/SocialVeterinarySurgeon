using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialVeterinarySurgeon.BL.Services.Interfaces;
using SocialVeterinarySurgeon.DAL;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.BL.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public EmployeeService(SocialVeterinarySurgeonDbContext db, IMapper mapper, ILogger<EmployeeService> logger) 
            : base(db, mapper, logger)
        {
        }

        public async Task<IEnumerable<Pet>> GetPets(int id)
        {
            var employee = await _db.Employees.Include(e => e.Pets)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                throw new Exception($"There is no employee with Id: {id}");
            }

            return employee.Pets;
        }
    }
}
