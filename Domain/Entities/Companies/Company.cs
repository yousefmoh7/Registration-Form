using Domain.Entities.Users;
using System.Collections.Generic;
namespace Domain.Entities.Companies
{
    public partial class Company : BaseEntity<int>
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Address { get; internal set; }

        public List<User> Users { get; internal set; }

        public Company(string name, string address, string description)
        {
            Update(name, address, description);
        }

    }
}