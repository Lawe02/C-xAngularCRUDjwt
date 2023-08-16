using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController : ControllerBase
{
    private List<Book> _books = new List<Book>();
    public BookService BookService = new BookService();

    private readonly ILogger<CrudController> _logger;

    public CrudController(ILogger<CrudController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        _books = BookService.RetrieveBooks();
        
        return Ok(_books);
    }
    [HttpDelete("Delete")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        _books = BookService.RetrieveBooks();

        var bookToDelete = _books.FirstOrDefault(x => x.BookId == id);
        _books.Remove(bookToDelete);

        BookService.SaveBooks(_books); 

        return Ok(_books);
    }
    [HttpPost("Create")]
    [Authorize]
    public IActionResult Create(Book book)
    {
        _books = BookService.RetrieveBooks();

        _books.Add(book);
        
        BookService.SaveBooks(_books);
        
        return Ok(); 
    }
    [HttpPut("Update")]
    [Authorize]
    public IActionResult Update(Book book) 
    {
        _books = BookService.RetrieveBooks();

        var bookToUpdate = _books.FirstOrDefault(x =>  x.BookId == book.BookId);
        bookToUpdate.Author = book.Author;
        bookToUpdate.Title = book.Title;
        bookToUpdate.PublicityDate = book.PublicityDate;

        BookService.SaveBooks(_books);

        return Ok(bookToUpdate);
    }
}
