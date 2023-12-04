namespace SatinAlmaStokYonetim.Web.Models.Message
{
    public class MessagePostDto
    {
        public bool Request { get; set; }
        public bool Success { get; set; }
        public bool Buying { get; set; }
        public bool Management { get; set; }
        public bool Committee { get; set; }
        public bool Accounting { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
