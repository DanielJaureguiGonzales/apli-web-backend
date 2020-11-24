using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Domain.Services.Communication;

namespace TrainingGain.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IReviewRepository _reviewRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ISubscriptionRepository subscriptionRepository, IHistoryRepository historyRepository, IReviewRepository reviewRepository)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _subscriptionRepository = subscriptionRepository;
            _historyRepository = historyRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<CustomerResponse> DeleteAsync(int id)
        {
            var existingCustomer = await _customerRepository.FindById(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            try
            {
                _customerRepository.Remove(existingCustomer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error ocurred while deleting customer: {ex.Message}");
            }
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var existingCustomer = await _customerRepository.FindById(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");
            return new CustomerResponse(existingCustomer);
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _customerRepository.ListAsync();
        }

        public async Task<CustomerResponse> SaveAsync(Customer customer)
        {
            try
            {
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(customer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error ocurred while saving customer: {ex.Message}");
            }
        }

        public async Task<CustomerResponse> UpdateAsync(int id, Customer customer)
        {
            var existingCustomer = await _customerRepository.FindById(id);
            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            existingCustomer.Description = customer.Description;

            try
            {
                _customerRepository.Update(existingCustomer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error ocurred while updating customer: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Customer>> ListBySubscriptionPlanId(int subscriptionplanId)
        {
            var subscription = await _subscriptionRepository.ListBySubscriptionPlanIdAsync(subscriptionplanId);
            var customer = subscription.Select(s => s.Customer).ToList();
            return customer;
        }
        public async Task<IEnumerable<Customer>> ListBySessionIdAsync(int sessionId)    
        {
            var histories = await _historyRepository.ListBySessionIdAsync(sessionId);
            var customers = histories.Select(s => s.Customer).ToList();
            return customers;
        }

        public async Task<IEnumerable<Customer>> ListBySpecialistIdAsync(int specialistId)
        {
            var histories = await _reviewRepository.ListBySpecialistIdAsync(specialistId);
            var customers = histories.Select(s => s.Customer).ToList();
            return customers;
        }
    }
}
