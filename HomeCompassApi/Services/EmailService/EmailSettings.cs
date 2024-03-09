namespace HomeCompassApi.Services.EmailService
{
    public class EmailSettings
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string GmailUsername { get; set; }
        public string GmailPassword { get; set; }
    }
}