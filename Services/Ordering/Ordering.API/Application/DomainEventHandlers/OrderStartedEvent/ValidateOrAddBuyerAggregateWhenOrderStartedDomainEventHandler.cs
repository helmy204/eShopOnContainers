using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.Services;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Application.DomainEventHandlers.OrderStartedEvent
{
    public class ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler
                                        : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IIdentityService _identityService;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler(ILoggerFactory logger,
                                                                             IBuyerRepository buyerRepository,
                                                                             IIdentityService identityService,
                                                                             IOrderingIntegrationEventService orderingIntegrationEventService)
        {
            // ... Parameter validations...
        }

        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            var cardTypeId = (orderStartedEvent.CardTypeId != 0) ?
                            orderStartedEvent.CardTypeId : 1;

            var userGuid = _identityService.GetUserIdentity();

            var buyer = await _buyerRepository.FindAsync(userGuid);
            bool buyerOriginallyExisted = (buyer == null) ? false : true;

            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(orderStartedEvent.UserId, orderStartedEvent.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                $"Payment Method on {DateTime.UtcNow}",
                orderStartedEvent.CardNumber,
                orderStartedEvent.CardSecurityNumber,
                orderStartedEvent.CardHolderName,
                orderStartedEvent.CardExpiration,
                orderStartedEvent.Order.Id);

            var buyerUpdated = buyerOriginallyExisted ? _buyerRepository.Update(buyer) :
                                                        _buyerRepository.Add(buyer);

            await _buyerRepository.UnitOfWork.SaveEntitesAsync();

            // Logging code using buyerUpdated info, etc.
        }
    }
}