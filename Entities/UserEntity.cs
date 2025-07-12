namespace DOTNETPROJECT.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string UserName { get; set; } = null!;
        public required string LastName { get; set; } = null!;
        public required string Phone { get; set; } = null!;
        
    }
}