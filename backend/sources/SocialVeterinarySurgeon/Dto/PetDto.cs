using System.ComponentModel.DataAnnotations;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.Dto
{
    public class PetDto : BaseDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public AnimalType Type { get; set; }

        [Required]
        public int OwnerId { get; set; }

    }
}
