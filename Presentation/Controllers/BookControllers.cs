using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        //private readonly IRepositoryManager  _manager;

        //public BooksController(IRepositoryManager manager)
        //{
        //    _manager = manager;
        //}
        //  yukarıdaki enjekte de repository kullanılıyordu  aşağıda service geçtik

        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            try
            {

                var books = _manager.BookService.GetAllBooks(false);  // trackchanges için false verdik
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }


        [HttpGet(" {id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {

            //  _repositoryContext.Books.Where(b=>b.Id.Equals(id)).SingleOrDefault();
            try
            {
                var book = _manager
                    .BookService
                    .GetOneBookById(id, false);

                if (book is null)
                {


                    return NotFound();


                }

                return Ok(book);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }




        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); // 400
                }

                _manager.BookService.CreateOneBook(book);


                return StatusCode(201, book);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }


        [HttpPut(" {id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); // 400
                }


                _manager.BookService.UpdateOneBook(id, book, true);

                return NoContent(); //204

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        [HttpDelete(" {id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {



                _manager.BookService.DeleteOneBook(id, false);


                return NoContent();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }




        }



    }
}
