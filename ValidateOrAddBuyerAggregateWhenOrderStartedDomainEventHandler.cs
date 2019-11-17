public class ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler
                                    : INotificationHandler<OrderStartedDomainEvent>
{
    private readonly ILoggerFactory _logger;
    private readonly IBuyerRepository<Buyer> _buyerRepository;
    private readonly IIdentityService _identityService;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler(ILoggerFactory logger,
                                                                         IBuyerRepository<Buyer> buyerRepository,
                                                                          IIdentityService identityService,
                                                                         IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        // ... Parameter validations...
    }

    public async Task Handle(OrderStartedDomainEvent orderStartedDomainEvent)
    {
        var cardTypeId = (orderStartedDomainEvent.CardTypeId != 0) ?
                        orderStartedDomainEvent.CardTypeId : 1;

        var userGuid = _identityService.GetUserIdentity();

        var buyer = await _buyerRepository.FindAsync(userGuid);
        bool buyerOriginallyExisted = (buyer == null) ? false : true;

        if(!buyerOriginallyExisted)
        {
            buyer = new Buyer(userGuid);
        }

        buyer.VerifyOrAddPaymentMethod(cardTypeId,
            $"Payment Method on {DateTime.UtcNow}",
            orderStartedDomainEvent.CardNumber,
            orderStartedDomainEvent.CardSecurityNumber,
            orderStartedDomainEvent.CardHolderName,
            orderStartedDomainEvent.CardExpiration,
            orderStartedDomainEvent.Order.Id);

        var buyerUpdated = buyerOriginallyExisted ? _buyerRepository.Update(buyer) :
                                                    _buyerRepository.Add(buyer);

        await _buyerRepository.UnitOfWork.SaveEntitesAsync();

        // Logging code using buyerUpdated info, etc.
    }
}