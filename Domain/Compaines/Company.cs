using Domain.Base;

namespace Domain.Departments
{
    public partial class Company : BaseEntity<int>
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Address { get; internal set; }


        public Company(string name, string address, string description)
        {
            Update(name, address, description);
        }

        public void Update(string name, string address, string description)
        {
            Name = name;
            Description = description;
            Address = address;
        }


    }
}