using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseWebSockets();

app.MapGet("/", async context =>
{
    // TODO
    
    
    if(!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return;
    }
    
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    
    Console.WriteLine("WebSocket connection established.");
    
    
    var c = 0;
    //while (true)
    while (c < 5)
    {
        try
        {
            await webSocket.SendAsync(
                Encoding.ASCII.GetBytes($".NET Rocks -> {DateTime.Now}"),
                WebSocketMessageType.Text,
                true, 
                CancellationToken.None);
            await Task.Delay(1000);
        }
        catch (WebSocketException ex) when (ex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
        {
            Console.WriteLine("Connection closed prematurely.");
        }
        c++;
        
    }
    
    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);


});

await app.RunAsync();