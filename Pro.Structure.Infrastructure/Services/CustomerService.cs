using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Services;

public class CustomerService : BaseService<Customer>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IFactory<Customer, CustomerModel> _customerFactory;
    private readonly IFactory<Project, ProjectModel> _projectFactory;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        ICustomerRepository customerRepository,
        IProjectRepository projectRepository,
        IFactory<Customer, CustomerModel> customerFactory,
        IFactory<Project, ProjectModel> projectFactory,
        IUnitOfWork unitOfWork
    )
        : base(customerRepository)
    {
        _customerRepository = customerRepository;
        _projectRepository = projectRepository;
        _customerFactory = customerFactory;
        _projectFactory = projectFactory;
        _unitOfWork = unitOfWork;
    }

    public override async Task<ServiceResponse<Customer>> AddAsync(Customer entity)
    {
        try
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                // Check if customer with same email exists
                if (await _customerRepository.ExistsByEmailAsync(entity.Email))
                    return ServiceResponse<Customer>.Fail(
                        "Customer with this email already exists"
                    );

                var success = await _customerRepository.AddAsync(entity);
                if (!success)
                    return ServiceResponse<Customer>.Fail("Failed to add customer");

                return ServiceResponse<Customer>.Ok(entity, "Customer added successfully");
            });
        }
        catch (Exception ex)
        {
            return ServiceResponse<Customer>.Fail($"Error adding customer: {ex.Message}");
        }
    }

    public override async Task<ServiceResponse<Customer>> UpdateAsync(Customer entity)
    {
        try
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var existingCustomer = await _customerRepository.GetByIdAsync(entity.Id);
                if (existingCustomer == null)
                    return ServiceResponse<Customer>.Fail("Customer not found");

                // Check if email is being changed and if new email already exists
                if (
                    existingCustomer.Email != entity.Email
                    && await _customerRepository.ExistsByEmailAsync(entity.Email)
                )
                {
                    return ServiceResponse<Customer>.Fail(
                        "Customer with this email already exists"
                    );
                }

                var success = await _customerRepository.UpdateAsync(entity);
                if (!success)
                    return ServiceResponse<Customer>.Fail("Failed to update customer");

                return ServiceResponse<Customer>.Ok(entity, "Customer updated successfully");
            });
        }
        catch (Exception ex)
        {
            return ServiceResponse<Customer>.Fail($"Error updating customer: {ex.Message}");
        }
    }

    public override async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                // Check if customer has any projects
                var customerProjects = await _projectRepository.GetProjectsByCustomerAsync(id);
                if (customerProjects.Any())
                {
                    return ServiceResponse<bool>.Fail(
                        "Cannot delete customer with active projects"
                    );
                }

                var success = await _customerRepository.DeleteAsync(id);
                if (!success)
                    return ServiceResponse<bool>.Fail("Failed to delete customer");

                return ServiceResponse<bool>.Ok(true, "Customer deleted successfully");
            });
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error deleting customer: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<CustomerModel>> GetByEmailAsync(string email)
    {
        try
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null)
                return ServiceResponse<CustomerModel>.Fail("Customer not found");

            return ServiceResponse<CustomerModel>.Ok(_customerFactory.CreateModel(customer));
        }
        catch (Exception ex)
        {
            return ServiceResponse<CustomerModel>.Fail($"Error retrieving customer: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> ExistsByEmailAsync(string email)
    {
        try
        {
            var exists = await _customerRepository.ExistsByEmailAsync(email);
            return ServiceResponse<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error checking customer existence: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetCustomerProjectsAsync(
        int customerId
    )
    {
        try
        {
            var projects = await _projectRepository.GetProjectsByCustomerAsync(customerId);
            var models = projects.Select(p => _projectFactory.CreateModel(p));
            return ServiceResponse<IEnumerable<ProjectModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(
                $"Error retrieving customer projects: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<IEnumerable<CustomerModel>>> GetAllAsync()
    {
        var result = await base.GetAllAsync();
        if (!result.Success)
            return ServiceResponse<IEnumerable<CustomerModel>>.Fail(result.Message);

        try
        {
            var models = result.Data.Select(c => _customerFactory.CreateModel(c));
            return ServiceResponse<IEnumerable<CustomerModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<CustomerModel>>.Fail(
                $"Error mapping customers: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<CustomerModel>> GetByIdAsync(int id)
    {
        var result = await base.GetByIdAsync(id);
        if (!result.Success)
            return ServiceResponse<CustomerModel>.Fail(result.Message);

        try
        {
            var model = _customerFactory.CreateModel(result.Data);
            return ServiceResponse<CustomerModel>.Ok(model);
        }
        catch (Exception ex)
        {
            return ServiceResponse<CustomerModel>.Fail($"Error mapping customer: {ex.Message}");
        }
    }
}
