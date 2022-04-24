using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

       
        public int AuthorId { get; set; }

        public DateTime PublishDate { get; set; }

        public double Price { get; set; }

    }
}
