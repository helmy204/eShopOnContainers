using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Ordering.API.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private string _connectionString = string.Empty;

        public OrderQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<OrderSummary>> GetOrdersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<OrderSummary>(@"SELECT o.[Id] as OrderNumber,
                        o.[OrderDate] as [date], os.[Name] as [status],
                        SUM(oi.units*oi.unitprice) as total,
                        FROM [ordering].[Orders] o
                        LEFT JOIN [ordering].[orderitems] oi ON o.Id = oi.orderid
                        LEFT JOIN [ordering].[orderstatus] os on o.OrderStatusId = os.Id
                        GROUP BY o.[Id], o.[OrderDate], os.[Name]");
            }
        }
    }
}
