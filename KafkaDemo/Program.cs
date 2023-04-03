// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;

Console.WriteLine("Hello, World!");


var conf = new ConsumerConfig
{
    AutoOffsetReset = AutoOffsetReset.Earliest
};


var config = new ProducerConfig
{
    BootstrapServers = "host1:9092",
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    var result = await producer.ProduceAsync("weblog", new Message<Null, string> { Value = "a log message" });

    producer.Flush(TimeSpan.FromSeconds(10));
}