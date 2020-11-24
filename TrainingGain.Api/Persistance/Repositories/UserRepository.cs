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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public  void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public  void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
