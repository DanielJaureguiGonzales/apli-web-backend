using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Domain.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<IEnumerable<Review>> ListByCustomerIdAsync(int customerId);
        Task<IEnumerable<Review>> ListBySpecialistIdAsync(int specialistId);    
        Task<ReviewResponse> AssignReviewAsync(int customerId, int specialistId, string description,int rank); 
    }
}
