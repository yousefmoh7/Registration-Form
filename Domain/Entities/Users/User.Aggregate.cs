namespace Domain.Entities.Users
{
    public partial class User
    {
        public void Update(string name
              , string address
              , string email
              , string password
              , int companyId)
        {
            Name = name;
            Address = address;
            Password = password;
            Email = email;
            CompanyId = companyId;
        }

    }
}