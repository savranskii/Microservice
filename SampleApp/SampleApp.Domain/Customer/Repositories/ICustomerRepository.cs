﻿using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Repositories;

public interface ICustomerRepository : IRepository<long, Entities.CustomerInfo>, IDisposable
{
    IUnitOfWork UnitOfWork { get; }
    
    Task<Entities.CustomerInfo?> GetByEmailAsync(string email);
    Task<IEnumerable<Entities.CustomerInfo>> GetAllAsync();
}
