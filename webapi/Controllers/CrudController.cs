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
    private List<Quote> _quotes = new List<Quote>();    
    public BookService BookService = new BookService();
    public QuoteService QuoteService = new QuoteService();

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
    public IActionResult Delete(Guid id)
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
        Guid guid = Guid.NewGuid();
        book.BookId = guid;

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

    [HttpGet("GetQuotes")]
    [Authorize]
    public IActionResult GetQuotes(Guid id)
    {
        _quotes = QuoteService.RetrieveQuotes();
        return Ok(_quotes);
    }
    [HttpPost("AddQuote")]
    [Authorize]
    public IActionResult AddQuote(Quote quote)
    {
        _quotes = QuoteService.RetrieveQuotes();
        _quotes.Add(quote);
        QuoteService.SaveQuotes(_quotes);
        return Ok();
    }
    [HttpDelete("DeleteQuote")]
    [Authorize]
    public IActionResult DeleteQuote(Guid id)
    {
        _quotes = QuoteService.RetrieveQuotes();
        var quoteToRemove = _quotes.FirstOrDefault(q => q.Id == id);
        if (quoteToRemove != null)
        {
            _quotes.Remove(quoteToRemove);
            QuoteService.SaveQuotes(_quotes);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
