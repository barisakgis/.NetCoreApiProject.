using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;
 

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager  _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            try
            {   

                var books = _manager.Book.GetAllBooks(false);  // trackchanges için false verdik
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }
            

        }


        [HttpGet(" {id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {

            //  _repositoryContext.Books.Where(b=>b.Id.Equals(id)).SingleOrDefault();
            try
            {
                var book =  _manager.Book.GetOneBookById(id,false);

                if (book is null)
                {


                    return NotFound();


                }

                return Ok(book);


            }
            catch (Exception ex)
            {
                throw  new Exception(ex.Message);

            }




        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null) {
                    return BadRequest(); // 400
                }
                 
                _manager.Book.CreateOneBook(book);
                _manager.Save();

                return StatusCode(201, book);


            }
            catch (Exception ex)
            {

                throw  new Exception(ex.Message)  ;
            }


        }


        [HttpPut(" {id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] Book book)
        {
            try
            {

                // kitap var mı varsa bulduk
                var entity = _manager
                     .Book.GetOneBookById(id,true);

                if (entity is null)
                    return NotFound();  // 404


                if (id != book.Id)
                    return BadRequest(); // 400
                 

                //  eski varlık = yeni varlık   //   yeni girdiklerimizi eskelere atıyoruz 

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

                return Ok(book);

            }
            catch (Exception ex)
            {

                throw  new Exception(ex.Message)  ;
            }


        }

        [HttpDelete(" {id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id") ]int id)
        {
            try
            {
                var entity = _manager
                    .Book.GetOneBookById(id,false);

                if (entity is null) 
                    return NotFound
                        (
                            new 
                            { 
                                statusCode= 404 ,
                                message =$"Book with id:{id} could not found" 
                             
                            }
                        
                        
                        );


                 _manager.Book.DeleteOneBook(entity);
                 _manager.Save();   

                return NoContent();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }
            



        }



    }
}
