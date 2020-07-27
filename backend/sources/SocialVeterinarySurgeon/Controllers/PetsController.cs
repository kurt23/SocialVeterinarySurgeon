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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly ILogger<PetsController> _logger;
        private readonly IMapper _mapper;

        public PetsController(IPetService petService, ILogger<PetsController> logger, IMapper mapper)
        {
            _petService = petService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<PetDto> Upsert(PetDto petDto)
        {
            var pet = await _petService.Upsert(_mapper.Map<Pet>(petDto));
            return _mapper.Map<PetDto>(pet);
        }

        [HttpGet("{id}")]
        public async Task<PetDto> GetById(int id)
        {
            return _mapper.Map<PetDto>(await _petService.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _petService.Delete(id);
        }

        [HttpGet]
        public async Task<IEnumerable<PetDto>> GetList()
        {
            var employees = await _petService.GetList();
            return _mapper.Map<IEnumerable<PetDto>>(employees);
        }
    }
}
