using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialVeterinarySurgeon.Dto
{
    public class EmployeeDto : BaseDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        public bool FromMediaInteractiva { get; set; }

        public List<PetDto> Pets { get; set; }
    }
}
