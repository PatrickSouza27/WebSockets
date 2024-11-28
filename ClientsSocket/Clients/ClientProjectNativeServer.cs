using System.Net.WebSockets;
using System.Text;

namespace ClientsSocket.Clients;

public static class ClientProjectNativeServer
{
    public static async Task Run()
    {
        using var ws = new System.Net.WebSockets.ClientWebSocket();

        await ws.ConnectAsync(new Uri("ws://localhost:5003"), CancellationToken.None);

        var buffer = new byte[256];


        while (ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("Client requested close.");
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                return;
            }
            else
                Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
        }
    }
    
}