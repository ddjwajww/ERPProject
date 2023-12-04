using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Entities
{
    public class MessageReceiver : IEntity
    {
        [Key]public int Id { get; set; }
        public int MessageId { get; set; }
        public bool Request { get; set; }
        public bool Success { get; set; }
        public bool Buying { get; set; }
        public bool Management { get; set; }
        public bool Committee { get; set; }
        public bool Accounting { get; set; }
        public Message Message { get; set; }
    }
}
