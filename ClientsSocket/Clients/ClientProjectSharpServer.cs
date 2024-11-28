

using WebSocketSharp;

namespace ClientsSocket.Clients;

public static class ClientProjectSharpServer
{
    public static void Run()
    {
        using var ws = new WebSocket("ws://127.0.0.1:7890/echo");
        //using var ws = new WebSocket("ws://127.0.0.1:7890/echoAll");


        Console.WriteLine("WS Client Started on");
        
        
        //usar o evento OnMessage para receber mensagens do servidor, lambda or function
        ws.OnMessage += (sender, e) =>
        {
            Console.WriteLine(e.Data);
        };
        
        //or
        //ws.OnMessage += Ws_OnMessage;
        
        ws.Connect();
        ws.Send("Hello World from ClientProjectSharpServer");
        
        Console.ReadKey();
    }
    
    private static void Ws_OnMessage(object? sender, MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
    }
    
    
}