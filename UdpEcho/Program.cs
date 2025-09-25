using System.Net;
using System.Net.Sockets;
using System.Text;

using var udp = new UdpClient(new IPEndPoint(IPAddress.Any, 5001));
Console.WriteLine("UDP echo on :5001");
while (true)
{
    var result = await udp.ReceiveAsync();
    Console.WriteLine($"UDP {result.RemoteEndPoint} {Encoding.UTF8.GetString(result.Buffer)}");
    var data = Encoding.UTF8.GetBytes("echo: " + Encoding.UTF8.GetString(result.Buffer));
    await udp.SendAsync(data, result.RemoteEndPoint);
}