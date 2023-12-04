using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Message
{
    public class UIMessageGetDto : IDto
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
