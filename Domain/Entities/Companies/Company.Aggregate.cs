namespace Domain.Entities.Companies
{
    public partial class Company
    {
        public void Update(string name, string address, string description)
        {
            Name = name;
            Description = description;
            Address = address;
        }

    }
}
