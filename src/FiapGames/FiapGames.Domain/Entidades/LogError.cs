namespace FiapGames.Domain.Entidades
{
    public class LogError
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Exception { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }
        public string Url { get; set; }
        public string TraceId { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
    }
}
