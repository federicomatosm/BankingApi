
using Banking.Account.Command.Domain;
using Banking.Account.Command.Application.Contracts.Persistence;
using Microsoft.Extensions.Options;
using Banking.Account.Command.Application.Models;
using MongoDB.Driver;

namespace Banking.AccountCommand.Infrastructure.Repositories
{
	public class EventStoreRepository : MongoRepository<EventModel>, IEventStoreRepository
	{

        public EventStoreRepository(IOptions<MongoSettings> options) : base(options)
        {
        }

        public async Task<IEnumerable<EventModel>> FindByAggregateIdentifier(string aggregateIdentifier)
        {
            FilterDefinition<EventModel>? filter = Builders<EventModel>.Filter.Eq(doc => doc.AggregateIdentifier, aggregateIdentifier);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}

