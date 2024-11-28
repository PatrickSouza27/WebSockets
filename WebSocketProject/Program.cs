using Fleck;

var server = new WebSocketServer("ws://0.0.0.0:8181");

var wsconnect = new List<IWebSocketConnection>();

// server.Start(ws=>
// {
//     ws.OnOpen = () => Console.WriteLine("Open!");
//     //ws.OnClose = () => Console.WriteLine("Close!");
//     ws.OnMessage = message =>
//     {
//         ws.Send(message);
//         Console.WriteLine(message);
//     };
//     
// });

server.Start(ws=>
{
    ws.OnOpen = () =>
    {
        Console.WriteLine("Open!");
        wsconnect.Add(ws);
    };
    //ws.OnClose = () => Console.WriteLine("Close!");
    ws.OnMessage = message =>
    {
        wsconnect.ForEach(x=> x.Send(message));
    };
    
});



WebApplication.CreateBuilder(args).Build().Run();
