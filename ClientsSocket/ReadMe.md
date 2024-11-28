### Event-Driven Architecture For Websockets


```csharp
public class ClientWantsToEchoServerDto : BaseDto
{
    public string messageContent { get; set; }
}

public class BaseDto
{
    public string messageType { get; set; }
}

public class ClientWantsToEchoServerHandler : BaseHandler<ClientWantsToEchoServerDto>
{
    public override Task Handle(ClientWantsToEchoServerDto dto, IWebSocketConnection socket)
    {
        return Task.Run(() =>
        {
            socket.Send(dto.messageContent);
        });
    }
}



server.Start(socket =>
{
    socket.OnOpen = () => Console.WriteLine("Open!");
    socket.OnClose = () => Console.WriteLine("Close!");
    socket.OnMessage = message =>
    {
        var dto = JsonConvert.DeserializeObject<BaseDto>(message);
        var handler = new HandlerFactory().GetHandler(dto);
        handler.Handle(dto, socket);
        
        // app.InvokeClientEventHandler(dto, socket);
    };
});
```

