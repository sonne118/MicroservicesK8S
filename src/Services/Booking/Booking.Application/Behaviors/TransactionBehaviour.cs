using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Booking.Application.IntegrationEvents;
using Booking.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Behaviors
{
    //The Method is used if we want to publish all the pending events at once
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly BookingDbContext _dbContext;
        private readonly IBookingIntegrationEventService _orderingIntegrationEventService;

        public TransactionBehaviour(BookingDbContext dbContext,
            IBookingIntegrationEventService bookingIntegrationEventService,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(BookingDbContext));
            _orderingIntegrationEventService = bookingIntegrationEventService ?? throw new ArgumentException(nameof(bookingIntegrationEventService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default(TResponse);

            try
            {
                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _dbContext.BeginTransactionAsync();

                    response = await next();

                    await _dbContext.CommitTransactionAsync();

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");

                    await _orderingIntegrationEventService.PublishEventsThroughEventBusAsync();
                });

                return response;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
