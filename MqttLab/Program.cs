using MQTTnet;
using MQTTnet.Client;
using System.Text;

var factory = new MqttFactory();
var client = factory.CreateMqttClient();

var options = new MqttClientOptionsBuilder()
    .WithTcpServer("127.0.0.1", 1883)
    .Build();

await client.ConnectAsync(options);

if (args.FirstOrDefault() == "sub")
{
    await client.SubscribeAsync("sensors/temp");
    client.ApplicationMessageReceivedAsync += e =>
    {
        Console.WriteLine("got: " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        return Task.CompletedTask;
    };
    Console.WriteLine("Subscribed, press Ctrl+C to exit.");
    await Task.Delay(Timeout.Infinite);
}
else
{
    var msg = new MqttApplicationMessageBuilder()
        .WithTopic("sensors/temp").WithPayload("23.4").Build();
    await client.PublishAsync(msg);
    Console.WriteLine("published");
}