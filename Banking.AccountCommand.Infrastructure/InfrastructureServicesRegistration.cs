using System;
using Banking.Account.Command.Application.Aggregates;
using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.AccountCommand.Infrastructure.KafkaEvents;
using Banking.AccountCommand.Infrastructure.Repositories;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Handlers;
using Banking.Cqrs.Core.Infrastructure;
using Banking.Cqrs.Core.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Banking.AccountCommand.Infrastructure
{
	public static class InfrastructureServicesRegistration
	{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<AccountOpenedEvent>();
            BsonClassMap.RegisterClassMap<AccountClosedEvent>();
            BsonClassMap.RegisterClassMap<FundsDepositedEvent>();
            BsonClassMap.RegisterClassMap<FundsWithdrawnEvent>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IEventProducer, AccountEventProducer>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<IEventStore, AccountEventStore>();
            services.AddTransient<IEventSourcingHandler<AccountAggregate>, AccountEventSourcingHandler>();


            return services;

        }
	}
}

