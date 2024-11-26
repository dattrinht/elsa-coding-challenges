namespace QuizSession.API;

public static class KafkaProducer
{
    public static async Task Produce<TKey, TValue>(string topic, TKey key, TValue value)
    {
        var host = "192.168.80.11:9092";
#if DEBUG
        host = "localhost:9092";
#endif
        var config = new ProducerConfig { BootstrapServers = host };
        using var p = new ProducerBuilder<string, string>(config).Build();
        try
        {
            var dr = await p.ProduceAsync(topic, new Message<string, string>
            {
                Key = JsonSerializer.Serialize(key),
                Value = JsonSerializer.Serialize(value)
            });
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
}