using BookApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        [MinLength(1)]
        [StringLength(255)]
        public string? FirstName { get; set; }
        [MinLength(1)]
        [StringLength(255)]
        public string? LastName { get; set; }
        [MinLength(1)]
        [StringLength(255)]
        public string? Email { get; set; }

        public int Age { get; set; }

        public DateTime DOB { get; set; }
        [MinLength(1)]
        [StringLength(1000)]
        public string? Address { get; set; }


    }
}
