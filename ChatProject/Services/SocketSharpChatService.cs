using System.Collections.Concurrent;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ChatProject.Services;

public class SocketSharpChatService : WebSocketBehavior
{
    private static readonly ConcurrentDictionary<string, SocketSharpChatService> Clients = new();
    private string _userId = string.Empty;
    protected override void OnOpen()
    {
        Console.WriteLine("Nova conexão estabelecida.");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var message = e.Data;

        if (message.StartsWith("/setid:"))
        {
            _userId = message[7..].Trim();
            if (!string.IsNullOrEmpty(_userId))
            {
                Clients[_userId] = this;
                Send($"Seu ID foi definido como {_userId}");
                Console.WriteLine($"ID {_userId} associado à conexão.");
            }
        }
    }
    
    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }
    
    // protected override void OnError(ErrorEventArgs e)
    // {
    //     base.OnError(e);
    // }
    //
    // protected override void OnPing(PingEventArgs e)
    // {
    //     base.OnPing(e);
    // }
    
}