using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Factories;

public class CustomerFactory : IFactory<Customer, CustomerModel>
{
    public Customer CreateEntity(CustomerModel model)
    {
        return new Customer
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Created = model.Created,
            Modified = DateTime.Now,
        };
    }

    public CustomerModel CreateModel(Customer entity)
    {
        return new CustomerModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            ProjectCount = entity.Projects.Count,
            Created = entity.Created,
            Modified = entity.Modified,
        };
    }
}
