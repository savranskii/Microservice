using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Repositories;

public interface ICustomerRepository : IRepository<long, Entities.Customer>
{
    Task<Entities.Customer?> GetByEmailAsync(string email);
    Task<IEnumerable<Entities.Customer>> GetAllAsync();
}
