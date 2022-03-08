using System;
using Banking.Account.Command.Application.Aggregates;
using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Domain;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Infrastructure;
using Banking.Cqrs.Core.Producers;

namespace Banking.AccountCommand.Infrastructure.KafkaEvents
{
	public class AccountEventStore : IEventStore
	{
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public AccountEventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEvents(string aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdentifier(aggregateId);

            if(eventStream == null || !eventStream.Any())
            {
                throw new Exception("The account does not exist");
            }

            return eventStream.Select(x => x.EventData).ToList();
           
        }

        public async Task SaveEvents(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventsStream = await _eventStoreRepository.FindByAggregateIdentifier(aggregateId);

            //check if event exist
            if(expectedVersion !=-1 && eventsStream.ElementAt(events.Count()-1).Version != expectedVersion)
            {
                throw new Exception("Errores de concurrencia");
            }

            var version = expectedVersion;
            foreach(var evt in events)
            {
                version += 1;
                evt.Version = version;

                var eventModel = new EventModel
                {
                    Timestamp = DateTime.Now,
                    AggregateIdentifier = nameof(AccountAggregate),
                    Version = version,
                    EventType = evt.GetType().Name,
                    EventData = evt
                };

                await _eventStoreRepository.InsertDocument(eventModel);
                _eventProducer.Produce(evt.GetType().Name, evt);
            }

            


        }
    }
}

