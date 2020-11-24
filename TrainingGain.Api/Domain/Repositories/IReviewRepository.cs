using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;

namespace TrainingGain.Api.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<IEnumerable<Review>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<Review>> ListBySpecialistIdAsync(int specialistId);    
        Task<Review> FindByCustomerIdAndSpecialistId(int customerId, int specialistId); 
        Task AddAsync(Review review);
        void Remove(Review review);
        Task AssingReview(int customerId, int specialistId, string description, int rank);  
    }
}
