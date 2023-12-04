using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace SASSTS.Model.Entities
{
    public class Message : IEntity
    {
        [Key] public int Id { get; set; }
        public long EmployeeId { get; set; } = 1;
        public string Text { get; set; }
        public DateTime MessageTime { get; set; } = DateTime.Now;   
        public string? Subject { get; set; }

        //Nav prop.
        public Employee Employee { get; set; }
        public MessageReceiver MessageReceiver { get; set; }
    }
}
