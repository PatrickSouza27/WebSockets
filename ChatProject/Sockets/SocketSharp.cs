using ChatProject.Services;
using WebSocketSharp.Server;

namespace ChatProject.Sockets;

public class SocketSharp
{
    private readonly WebSocketServer Server;
    
    public SocketSharp(string address, int port)
    {
        Server = new WebSocketServer($"ws://{address}:{port}");
        
        Server.AddWebSocketService<SocketSharpChatService>("/chat");
    }
}