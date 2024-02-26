namespace Booky.Models
{
    public class Book
    { public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public byte CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime Addon {get; set; }

        public Book()
        {
            Addon = DateTime.Now;
        }
    }
}
