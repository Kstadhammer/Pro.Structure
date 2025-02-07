using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Pro.Structure.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all customers",
        Description = "Retrieves a list of all customers"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<CustomerModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAll()
    {
        var result = await _customerService.GetAllAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get customer by ID",
        Description = "Retrieves a specific customer by their ID"
    )]
    [ProducesResponseType(typeof(ServiceResponse<CustomerModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _customerService.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("email/{email}")]
    [SwaggerOperation(
        Summary = "Get customer by email",
        Description = "Retrieves a specific customer by their email address"
    )]
    [ProducesResponseType(typeof(ServiceResponse<CustomerModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _customerService.GetByEmailAsync(email);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id:int}/projects")]
    [SwaggerOperation(
        Summary = "Get customer projects",
        Description = "Retrieves all projects for a specific customer"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerProjects(int id)
    {
        var result = await _customerService.GetCustomerProjectsAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create customer", Description = "Creates a new customer")]
    [ProducesResponseType(typeof(ServiceResponse<Customer>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Customer customer)
    {
        var exists = await _customerService.ExistsByEmailAsync(customer.Email);
        if (exists.Data)
            return BadRequest(
                ServiceResponse<Customer>.Fail("Customer with this email already exists")
            );

        var result = await _customerService.AddAsync(customer);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update customer", Description = "Updates an existing customer")]
    [ProducesResponseType(typeof(ServiceResponse<Customer>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Customer customer)
    {
        if (id != customer.Id)
            return BadRequest("ID mismatch");

        var result = await _customerService.UpdateAsync(customer);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete customer", Description = "Deletes a specific customer")]
    [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _customerService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
