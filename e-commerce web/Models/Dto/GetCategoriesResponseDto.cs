using e_commerce_web.Models.Domain;

namespace e_commerce_web.Models.Dto
{
    public class GetCategoriesResponseDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
