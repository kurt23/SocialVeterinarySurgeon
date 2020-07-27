using System.Collections.Generic;
using System.Threading.Tasks;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.BL.Services.Interfaces
{
    public interface IPetService
    {
        Task<Pet> Upsert(Pet entity);
        Task Delete(int id);
        Task<Pet> GetById(int id);
        Task<IEnumerable<Pet>> GetList();
    }
}
