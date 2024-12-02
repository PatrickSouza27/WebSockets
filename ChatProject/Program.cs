using ChatProject.Sockets;var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var socketSharp = new SocketSharp("ws://127.0.0.1", 7890);

app.Run();