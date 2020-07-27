using System.Collections.Generic;

namespace SocialVeterinarySurgeon.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public bool FromMediaInteractiva { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
