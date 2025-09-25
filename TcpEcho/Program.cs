using System.Net;
using System.Net.Sockets;
using System.Text;

var listener = new TcpListener(IPAddress.Any, 5000);
listener.Start();
Console.WriteLine("TCP echo on :5000");
while (true)
{
    _ = HandleAsync(await listener.AcceptTcpClientAsync());
}

static async Task HandleAsync(TcpClient c)
{
    using var client = c;
    var stream = client.GetStream();
    var buf = new byte[4096];
    int read;
    while ((read = await stream.ReadAsync(buf)) > 0)
    {
        var payload = Encoding.UTF8.GetString(buf.AsSpan(0, read));
        var resp = Encoding.UTF8.GetBytes("echo: " + payload);
        await stream.WriteAsync(resp);
    }
}