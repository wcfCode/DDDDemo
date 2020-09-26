using System;
using System.Collections.Generic;
using System.Text;
using DDD.Domain.OrderAggregate;
using DDD.Infrastructure.Core;
namespace DDD.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, DomainContext>, IOrderRepository
    {
        public OrderRepository(DomainContext context) : base(context)
        {
        }
    }
}
