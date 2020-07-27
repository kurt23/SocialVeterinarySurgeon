namespace SocialVeterinarySurgeon.Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }

        public AnimalType Type { get; set; }

        public int OwnerId { get; set; }
        
        public Employee Owner { get; set; }

    }
}
