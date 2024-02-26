using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booky.Models
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public byte CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
