﻿using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TeleAppBot.Domain.Events;
using TeleAppBot.Domain.Mensageria;

namespace TeleAppBot.Infrastructure.Mensageria
{
    public class KafkaService : IKafkaService, IDisposable
    {
        private readonly KafkaConfig _config;
        private IProducer<string, string> _producer;
        private IConsumer<string, string> _consumer;
        private bool _disposed;

        private readonly Dictionary<string, string> _nomeTopicos;

        public KafkaService(IOptions<KafkaConfig> options)
        {
            _config = options.Value;

            _nomeTopicos = new Dictionary<string, string>
            {
                { nameof(EnviarMensagemTextoEvent), _config.TopicoMensagemTexto },
                { nameof(EnviarMensagemMidiaEvent), _config.TopicoMensagemMidia }
            };

            _disposed = false;
        }

        private string ObterTopico<T>() => _nomeTopicos[typeof(T).Name];

        public T ConsumirMensagem<T>(CancellationToken tokenCancelamento = default) where T : Evento
        {
            if (_consumer is null)
            {
                var consumerConfig = new ConsumerConfig
                {
                    BootstrapServers = _config.Broker,
                    GroupId = _config.ConsumerGroup,
                    SaslMechanism = SaslMechanism.Plain,
                    SecurityProtocol = SecurityProtocol.SaslSsl,
                    SaslUsername = _config.Username,
                    SaslPassword = _config.Password
                };

                Console.WriteLine(_config.Broker);
                Console.WriteLine(_config.ConsumerGroup);

                _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
                _consumer.Subscribe(ObterTopico<T>());
            }

            var consumeResult = _consumer.Consume(tokenCancelamento);

            if (consumeResult?.Message?.Value is null)
                return default;

            return JsonConvert.DeserializeObject<T>(consumeResult.Message.Value);
        }

        public async Task EnviarMensagem<T>(T message) where T : Evento
        {
            if (_producer is null)
            {
                var producerConfig = new ProducerConfig
                {
                    BootstrapServers = _config.Broker,
                    SaslMechanism = SaslMechanism.Plain,
                    SecurityProtocol = SecurityProtocol.SaslSsl,
                    SaslUsername = _config.Username,
                    SaslPassword = _config.Password
                };

                _producer = new ProducerBuilder<string, string>(producerConfig).SetValueSerializer(Serializers.Utf8).Build();
            }

            var json = JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var topico = ObterTopico<T>();

            await _producer.ProduceAsync(topico, new Message<string, string> { Value = json });
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            if (_producer is not null)
                _producer.Dispose();

            if (_consumer is not null)
                _consumer.Dispose();

            GC.SuppressFinalize(this);

            _disposed = true;
        }
    }
}
