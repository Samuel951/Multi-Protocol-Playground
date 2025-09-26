using System.Text;
using MQTTnet;
using MQTTnet.Client;

var factory = new MqttFactory();
var client = factory.CreateMqttClient();

var options = new MqttClientOptionsBuilder()
    .WithTcpServer("test.mosquitto.org", 1883)   // or your broker
    .Build();

await client.ConnectAsync(options);

if (args.FirstOrDefault() == "sub")
{
    await client.SubscribeAsync("sensors/temp");
    client.ApplicationMessageReceivedAsync += e =>
    {
        Console.WriteLine("got: " + Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
        return Task.CompletedTask;
    };
    Console.WriteLine("Subscribed. Ctrl+C to exit.");
    await Task.Delay(Timeout.Infinite);
}
else
{
    var msg = new MqttApplicationMessageBuilder()
        .WithTopic("sensors/temp")
        .WithPayload("23.4")
        .Build();

    await client.PublishAsync(msg);
    Console.WriteLine("published");
}