namespace SatinAlmaStokYonetim.Web.Models.Todo
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Active { get; set; }
        public int EmployeeId { get; set; }
    }
}
