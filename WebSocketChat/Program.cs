var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();
var clients = new HashSet<System.Net.WebSockets.WebSocket>();

app.Map("/ws", async ctx =>
{
    if (ctx.WebSockets.IsWebSocketRequest)
    {
        using var ws = await ctx.WebSockets.AcceptWebSocketAsync();
        lock (clients) clients.Add(ws);
        var buffer = new byte[4096];
        while (ws.State == System.Net.WebSockets.WebSocketState.Open)
        {
            var res = await ws.ReceiveAsync(buffer, CancellationToken.None);
            if (res.MessageType == System.Net.WebSockets.WebSocketMessageType.Close) break;
            var segment = new ArraySegment<byte>(buffer, 0, res.Count);
            List<System.Net.WebSockets.WebSocket> snapshot;
            lock (clients) snapshot = clients.ToList();
            foreach (var c in snapshot)
                if (c.State == System.Net.WebSockets.WebSocketState.Open)
                    await c.SendAsync(segment, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
        }
        lock (clients) clients.Remove(ws);
    }
    else ctx.Response.StatusCode = 400;
});

app.Run();