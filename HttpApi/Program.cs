var builder = WebApplication.CreateBuilder(args);

// Enable HTTP/2+3 on Kestrel if you want (requires TLS for h2/h3):
// builder.WebHost.ConfigureKestrel(o => {
//     o.ListenAnyIP(8000); // HTTP/1.1
//     o.ListenAnyIP(8443, lo => {
//         lo.UseHttps("../localhost.pfx", "password"); // create dev cert
//         lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2AndHttp3;
//     });
// });

var app = builder.Build();

app.MapGet("/health", () => Results.Json(new { ok = true }));
app.MapPost("/echo", async (HttpContext ctx) => {
    using var reader = new StreamReader(ctx.Request.Body);
    var body = await reader.ReadToEndAsync();
    return Results.Json(new { you_sent = body });
});

app.Run();