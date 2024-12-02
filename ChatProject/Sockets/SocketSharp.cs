using ChatProject.Services;
using WebSocketSharp.Server;

namespace ChatProject.Sockets;

public class SocketSharp
{
    private readonly WebSocketServer Server;
    
    public SocketSharp(string address, int port)
    {
        Server = new WebSocketServer($"ws://127.0.0.1:7890");
        
        Server.AddWebSocketService<SocketSharpChatService>("/chat");
        
        Server.Start();
        
        Server.Log.Output = (data, message) => Console.WriteLine(message);
        
        Console.ReadKey();
        Server.Stop();

    }
}