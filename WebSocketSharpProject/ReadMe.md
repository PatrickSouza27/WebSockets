### Pacote Utilizado


* https://github.com/sta/websocket-sharp
- [WebSocketSharp](https://sta.github.io/websocket-sharp/) - Uma implementação de cliente e servidor WebSocket para o protocolo de comunicação de duas vias em uma única conexão TCP.
```bash
dotnet package WebSocketSharp
```

### Exemplo de uso

```csharp
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Send(e.Data);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var wssv = new WebSocketServer("ws://
        wssv.AddWebSocketService<Echo>("/Echo");
        wssv.Start();
        Console.ReadKey();
        wssv.Stop();
    }
}
```


