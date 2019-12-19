using System;

namespace Ordering.API.Application.Queries
{
    public class OrderSummary
    {
        public int ordernumber { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public double total { get; set; }
    }
}
