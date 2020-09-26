using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Domain;
using DDD.Domain.Events;
using DotNetCore.CAP;
using DDD.API.Application.IntegrationEvents;

namespace DDD.API.Application.DomainEventHandlers
{
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        ICapPublisher _capPublisher;
        public OrderCreatedDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capPublisher.PublishAsync("OrderCreated", new OrderCreatedIntegrationEvent(notification.Order.Id));
        }
    }
}
