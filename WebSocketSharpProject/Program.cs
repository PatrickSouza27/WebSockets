// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using WebSocketSharp;
using WebSocketSharp.Server;

var server = new WebSocketServer("ws://127.0.0.1:7890");

server.AddWebSocketService<Echo>("/echo");
server.AddWebSocketService<EchoAll>("/echoAll");
server.AddWebSocketService<EchoOne>("/echoOne");
server.Start();



Console.WriteLine("WS Server Started on ws://"+ server.Address + ":" + server.Port);

Console.ReadKey();
server.Stop();



class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Message Received from echo client : " + e.Data);
        //Send(e.Data);
    }
}

class EchoAll : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Message Received from echo all client : " + e.Data);
        Sessions.Broadcast(e.Data);
        
    }
}

class EchoOne : WebSocketBehavior
{
    private static readonly ConcurrentDictionary<string, EchoOne> Clients = new();
    private string name;
    
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Message Received from One Client : " + e.Data);
        //Sessions.Broadcast(e.Data);
        Sessions.Broadcast(Sessions.Count.ToString());
    }
    
    protected override void OnOpen()
    {
        Send("Nova conexão estabelecida com Echo One.");
        Sessions.Broadcast(Sessions.Count.ToString());
        var connnectionNumber = new Random().Next(1, 100);
        
        Console.WriteLine(connnectionNumber);
        Send("Sua connection é " + connnectionNumber);
        
        //("1. Definir ID: /setid:<SeuID>"); // pode ser pego pelo context
        //("2. Enviar mensagem para um usuário: /msg:<IDDestino>:<Mensagem>");
        // if (Clients.TryGetValue(targetId, out var targetClient))
        // {
        //     targetClient.Send($"Mensagem de {_userId}: {msgToSend}");
        // }
        // else
        // {
        //     Send($"Usuário {targetId} não encontrado.");
        // }
        // targetClient.Send($"Mensagem de {_userId}: {msgToSend}");
        // Send($"Mensagem enviada para {targetId}: {msgToSend}");
    }
}