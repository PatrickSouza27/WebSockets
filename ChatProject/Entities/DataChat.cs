namespace ChatProject.Entities;

public class DataChat
{
    public string Message { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
    public string User { get; set; }
    public string Destino { get; set; }
    
    public DataChat(string message, string user, string destino)
    {
        Message = message;
        User = user;
        Destino = destino;
    }
}