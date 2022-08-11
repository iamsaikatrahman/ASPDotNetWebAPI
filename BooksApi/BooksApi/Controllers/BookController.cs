using BooksApi.Interfaces.Manager;
using BooksApi.Manager;
using BooksApi.Model;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BooksApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : BaseController
    {
        IBookManager _bookManager;

        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            //var books = _dbContext.Books.ToList();
            
            try
            {

                var books = _bookManager.GetAll().ToList();
                //return  Ok(books);
                return CustomResult("Data load successfully", books);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            try
            {
                var book = _bookManager.GetById(id);
                if (book == null)
                {
                    //return NotFound();
                    return CustomResult("Book Not Found", HttpStatusCode.NotFound);
                }
                //return Ok(book);
                return CustomResult("Book is found", book);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetAllBooksAscending()
        {
            try
            {
                var books = _bookManager.GetAll().OrderBy(c => c.CreatedDate).ToList();
                //return Ok(books);
                return CustomResult("All books are ascending order by date", books);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetAllBooksDescending()
        {
            try
            {
                var books = _bookManager.GetAll().OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.Title).ToList();
                //return Ok(books);
                return CustomResult("Order by descending order and title", books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("title")]
        public IActionResult GetByTitle(string title) 
        {
            try
            {
                var books = _bookManager.GetAll(title);
                return CustomResult("Data loaded done", books);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("text")]
        public IActionResult SearchBook(string text)
        {
            try
            {
                var books = _bookManager.SearchBook(text);
                return CustomResult("Searching Books", books);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddBooks(Book book)
        {
            try
            {
                book.CreatedDate = DateTime.Now;
                //_dbContext.Books.Add(book);
                //bool isSaved =  _dbContext.SaveChanges() > 0;
                bool isSaved = _bookManager.Add(book);
                if (isSaved)
                {
                    //return Created("", book);
                    return CustomResult("Book has been created", book);
                }
                //return BadRequest("Book save is failed");
                return CustomResult("Book save is failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public IActionResult Edit(Book book)
        {
            try
            {
                if (book.Id == 0)
                {
                    return CustomResult("Book Not Found", HttpStatusCode.BadRequest);
                }
                bool isUpdate = _bookManager.Update(book);
                if (isUpdate)
                {
                    //return Ok("Book Update sccuessfull");
                    return CustomResult("Book Update successfull", book);
                }
                //return BadRequest("Book update failed");
                return CustomResult("Book Update failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                var book = _bookManager.GetById(id);
                if (book == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                bool isDeleted = _bookManager.Delete(book);
                if (isDeleted)
                {
                    //return Ok("Book is Deleted successfully");
                    return CustomResult("Book is Deleted successfully");
                }
                return CustomResult("Book is deleted failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
