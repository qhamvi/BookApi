using BookApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string? Title { get; set; }
        
        [MinLength(1)]
        [StringLength(1000)]
        
        public string? Description { get; set; }
        public int AuthorId { get; set; }

        public DateTime PublishDate { get; set; }

        public double Price { get; set; }
        
    }
}
