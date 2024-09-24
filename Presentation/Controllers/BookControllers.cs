using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]  
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
        public async Task< IActionResult> GetAllBookAsync()
        {
              
                var books = await _manager.BookService.GetAllBooksAsync(false);  // trackchanges için false verdik
                return Ok(books);
             

        }


        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        { 
            //  _repositoryContext.Books.Where(b=>b.Id.Equals(id)).SingleOrDefault();
              
                var book = await _manager
                    .BookService
                    .GetOneBookByIdAsync(id, false);
             
                return Ok(book);

             
        }


        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public  async Task< IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        { 
              var book=  await _manager.BookService.CreateOneBookAsync(bookDto);
             
                return StatusCode(201, book);
             

        }


       
        [ServiceFilter(typeof (ValidationFilterAttribute) )]
        [HttpPut(" {id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        { 
                  
               await  _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

                return NoContent(); //204
             
        }

        [HttpDelete(" {id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            
               await _manager.BookService.DeleteOneBookAsync(id, false);


                return NoContent();
             

        }



    }
}
