using Banking.Account.Command.Application.Models;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Banking.AccountCommand.Infrastructure.KafkaEvents
{
	public class AccountEventProducer : IEventProducer
	{
        public KafkaSettings _kafkaSettings { get; set; }

        public AccountEventProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }

        public void Produce(string topic, BaseEvent @event)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}"
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var classEvent = @event.GetType();
            string value = JsonConvert.SerializeObject(@event);

            var message = new Message<Null, string> { Value = value };

            producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
        }
    }
}

