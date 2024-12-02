using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Claims;
using ChatProject.Entities;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace ChatProject.Services;

public class SocketSharpChatService : WebSocketBehavior
{
    private static readonly ConcurrentDictionary<string, SocketSharpChatService> Clients = new();
    private string _userId = string.Empty;
    protected override void OnOpen()
    {
        Console.WriteLine("Nova conexão estabelecida.");
        Console.WriteLine("Context.QueryString conexao aberta por : " + Context.QueryString["name"]);
        Console.WriteLine("Token With Headers : " + Context.Headers["Token"]);
        
    }

    /*
     {
        "message" : "Ola",
        "user" : "edu",
        "destino" : "patrick"
        }
     */
    protected override void OnMessage(MessageEventArgs e)
    {
        var data = JsonConvert.DeserializeObject<DataChat>(e.Data);
        
        
        
        Console.WriteLine("Context.Host : " + Context.Host);
        Console.WriteLine("Context.UserEndPoint : " + Context.UserEndPoint);
        Console.WriteLine("Context.User : " + Context.User);
        Console.WriteLine("Context.Origin : " + Context.Origin);
        Console.WriteLine("Context.Headers : " + Context.Headers["Connection"]);
        Console.WriteLine("Context.Headers.AllKeys : " + Context.Headers.AllKeys);
        Console.WriteLine("Context.QueryString : " + Context.QueryString);
        
        Clients.TryAdd(data.User, this);
        if(Clients.TryGetValue(data.Destino, out var client))
        {
            client.Send("Mensagem recebida");
            Send("mensagem enviada com sucesso");
        }
        else
        {
            Send("usuario nao existe");
        }
        
        
    }
    
}