using Infrastructure.Models;

namespace SASSTS.Model.Dtos.Category
{
    public class CategoryGetDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
