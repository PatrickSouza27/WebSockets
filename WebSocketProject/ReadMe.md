### Pacote Utilizado 

- [WebSocket](https://github.com/statianzo/Fleck) - Uma implementação de cliente e servidor WebSocket para o protocolo de comunicação de duas vias em uma única conexão TCP.

### Instalação

```bash
dotnet add package Fleck --version
```

### Exemplo de uso

```csharp
using System;
using Fleck;
var server = new WebSocketServer("ws://0.0.0.0:8181");
server.Start(socket =>
{
    socket.OnOpen = () => Console.WriteLine("Open!");
    socket.OnClose = () => Console.WriteLine("Close!");
    socket.OnMessage = message => socket.Send(message);
});
```

