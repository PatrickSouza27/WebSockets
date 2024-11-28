namespace ChatProject.Entities;

public class DataChat
{
    public string Message { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
    public string User { get; set; }
    
    public DataChat(string message, string user)
    {
        Message = message;
        User = user;
    }
}