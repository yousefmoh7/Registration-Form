using System;

namespace API.DTOs.Users
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}