using System.Collections.Generic;
using System.Threading.Tasks;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.BL.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> Upsert(Employee entity);
        Task Delete(int id);
        Task<Employee> GetById(int id);
        Task<IEnumerable<Employee>> GetList();

        Task<IEnumerable<Pet>> GetPets(int id);
    }
}
