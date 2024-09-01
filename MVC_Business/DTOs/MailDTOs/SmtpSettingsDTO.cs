namespace BlogMainStructure.Business.DTOs.MailDTOs
{
    public class SmtpSettingsDTO
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
    }
}
