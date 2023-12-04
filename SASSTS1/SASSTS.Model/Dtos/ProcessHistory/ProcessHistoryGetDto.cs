using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Model.Dtos.ProcessHistory
{
    public class ProcessHistoryGetDto : IDto
    {
        public DateTime ProcessTime { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDetail { get; set; }
    }
}
