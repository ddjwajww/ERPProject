using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Todo
{
    public class TodoGetDto : IDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Active { get; set; }
        public int EmployeeId { get; set; }
    }
}
