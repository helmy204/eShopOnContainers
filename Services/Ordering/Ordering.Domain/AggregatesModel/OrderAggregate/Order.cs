using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate
{
    // COMPATIBLE WITH ENTITY FRAMEWORK CORE 2.0
    // Entity is a custom base class with the ID
    public class Order : Entity, IAggregateRoot
    {
        private DateTime _orderDate;
        public Address Address { get; private set; }
        private int? _buyerId; // FK pointing to a different aggregate root

        public OrderStatus OrderStatus { get; private set; }
        private int _orderStatusId;

        private string _description;
        private int? _paymentMethodId;

        private readonly List<OrderItem> _orderItems;

        public Order(string userId, Address address, int cardTypeId,
                     string cardNumber, string cardSecurityNumber,
                     string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null)
        {
            _orderItems = new List<OrderItem>();
            _buyerId = buyerId;
            _paymentMethodId = paymentMethodId;
            _orderStatusId = OrderStatus.Submitted.Id;
            _orderDate = DateTime.UtcNow;
            Address = address;

            // ... Additional code ...
        }

        public void AddOrderItem(int productId, string productName,
                                 decimal unitPrice, decimal discount,
                                 string pictureUrl, int units = 1)
        {
            // ...
            // Domain rules/logic for adding the OrderItem to the order
            // ...

            var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

            _orderItems.Add(orderItem);
        }

        // ...
        // Additional methods with domain rules/logic related to the Order aggregate
        // ...

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;



        // ... Additional code

        private void AddOrderStartedDomainEvent(string userId, string userName, int cardTypeId, string cardNumber,
                string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId,
                                                                      cardNumber, cardSecurityNumber,
                                                                      cardHolderName, cardExpiration);

            this.AddDomainEvent(orderStartedDomainEvent);
        }
    }
}
