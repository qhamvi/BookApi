#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookApi.Models;
using BookApi.Dtos;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
       
        private readonly BookContext _context;
        
        private static BookDto BookToDTO(Book book) =>
            new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
                PublishDate = book.PublishDate,
                Price = book.Price
            };

        public BooksController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        { var books = await _context.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();

            return books; 
            
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return BookToDTO(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(bookDto).State = EntityState.Modified;

            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            //Cap nhat tieu de, mo ta, gia
            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookExists(id)) { 
            {
                    return NotFound();
            }
               
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                AuthorId = bookDto.AuthorId,
                PublishDate = DateTime.Now,
                Price = bookDto.Price
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetBook", new { id = book.Id }, book);
            return CreatedAtAction(
                nameof(GetBook), 
                new {id = book.Id},
                BookToDTO(book));
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
