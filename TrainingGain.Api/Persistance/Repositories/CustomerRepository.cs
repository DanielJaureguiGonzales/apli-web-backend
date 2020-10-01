using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Persistance.Context;
using TrainingGain.Api.Domain.Repositories;

namespace TrainingGain.Api.Persistance.Repositories
{
    public class CustomerRepository : BaseRepository,ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
        }

        public async Task<Customer> FindById(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public void Remove(Customer customer)
        {
            _context.Customer.Remove(customer);
        }

        public void Update(Customer customer)
        {
            _context.Customer.Update(customer);
        }
    }
}
