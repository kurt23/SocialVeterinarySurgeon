using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialVeterinarySurgeon.BL.Services.Interfaces;
using SocialVeterinarySurgeon.DAL;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.BL.Services
{
    public class PetService : BaseService<Pet>, IPetService
    {
        public PetService(SocialVeterinarySurgeonDbContext db, IMapper mapper, ILogger<BaseService<Pet>> logger) 
            : base(db, mapper, logger)
        {
        }
    }
}
