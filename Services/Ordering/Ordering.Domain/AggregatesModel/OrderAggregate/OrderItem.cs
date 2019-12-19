using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : Entity
    {
        private string _productName;
        private string _pictureUrl;
        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        public int ProductId { get; private set; }

        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units)
        {
            this.ProductId = productId;

            this._productName = productName;
            this._unitPrice = unitPrice;
            this._discount = discount;
            this._units = units;
            this._pictureUrl = pictureUrl;
        }
    }
}
