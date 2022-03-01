using System;
using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Infrastructure
{
	public interface IEventStore
	{
        public Task SaveEvents(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
		public Task<List<BaseEvent>> GetEvents(string aggregateId);
	}
}

