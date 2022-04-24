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
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorContext _context;
        
       
        private static AuthorDto AuthorToDTO(Author author) =>
           
            new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Age = author.Age,
                DOB = author.DOB,
                Address = author.Address,
                
        };
        


        public AuthorsController(AuthorContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            
            return await _context.Authors
                .Select(x => AuthorToDTO(x))
                .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return AuthorToDTO(author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }

            //_context.Entry(authorDto).State = EntityState.Modified;

            var author = await _context.Authors.FindAsync(id);
            if(author == null)
            {
                return NotFound();
            }
            //Cap nhat FirstName, LastName,Email, Age, DOB
            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.Email = authorDto.Email;
            author.Age = authorDto.Age;
            author.DOB = authorDto.DOB;
            author.Address = authorDto.Address;

           
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AuthorExists(id)) { 
            {
                    return NotFound();
            }
               
            }

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Email = authorDto.Email,
                Age = authorDto.Age,
                DOB = authorDto.DOB,
                Address = authorDto.Address
        };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAuthor), 
                new {id = author.Id},
                AuthorToDTO(author));
        }

        // DELETE: api/Authors/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
