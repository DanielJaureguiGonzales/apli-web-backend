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
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public async Task AssingReview(int customerId, int specialistId, string description, int rank)
        {
            Review review = await FindByCustomerIdAndSpecialistId(customerId, specialistId);
            if (review == null)
            {
                review = new Review { CustomerId = customerId, SpecialistId = specialistId, Description = description, Rank= rank };
                await AddAsync(review);
            }
            review.Description = description;
            review.Rank = rank;
        }

        public async Task<Review> FindByCustomerIdAndSpecialistId(int customerId, int specialistId)
        {
            return await _context.Reviews.FindAsync(customerId, specialistId);
        }

        public async Task<IEnumerable<Review>> ListAsync()
        {
            return await _context.Reviews.Include(r => r.Customer).Include(r => r.Specialist).ToListAsync();
        }

        public async Task<IEnumerable<Review>> ListByCustomerIdAsync(int customerId)
        {
            return await _context.Reviews.Where(r=>r.CustomerId==customerId).Include(r => r.Customer).Include(r => r.Specialist).ToListAsync();
        }

        public async Task<IEnumerable<Review>> ListBySpecialistIdAsync(int specialistId)
        {
            return await _context.Reviews.Where(r => r.SpecialistId == specialistId).Include(r => r.Customer).Include(r => r.Specialist).ToListAsync();

        }

        public void Remove(Review review)
        {
            _context.Reviews.Remove(review);
        }
    }
}
