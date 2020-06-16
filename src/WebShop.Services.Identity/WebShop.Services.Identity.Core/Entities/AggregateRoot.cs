using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Core.Entities
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> Events => _events;
        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }

        public void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvent() => _events.Clear();
    }
}
