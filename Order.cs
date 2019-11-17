public class Order
{
    public void StartOrder()
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, // Order object
                                                                 cartTypeId, cardNumber,
                                                                 cardSecurityNumber,
                                                                 cardHolderName,
                                                                 cardExpiration);

        this.AddDomainEvent(orderStartedDomainEvent);
    }
}