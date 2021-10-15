using Domain.Base;
using Domain.Departments;
using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public partial class User : BaseEntity<int>
    {
        public User(string name
            , string email
            , string address
            , string password
            , int companyId)
        {
            this.Update(name, address, email, password,companyId);
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Address { get; private set; }
        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; }
    }
}