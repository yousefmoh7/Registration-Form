namespace Domain.DTOs.Users
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}