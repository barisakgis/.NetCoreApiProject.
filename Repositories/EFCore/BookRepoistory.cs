using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class BookRepoistory : RepositoryBase<Book>, IBookRepository
    {
        public BookRepoistory(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);
         
        public void DeleteOneBook(Book book) => Delete(book);


        //public async Task<IEnumerable<Book>> GetAllBooksAsync( bool trackChanges) => 
        //   await FindAll(trackChanges)
        //    .OrderBy(x => x.Id)             
        //    .ToListAsync(); 

        //public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        //{
        //   var books = await FindAll(trackChanges)
        //   .OrderBy(x => x.Id)            
        //   .ToListAsync();

        //    return PagedList<Book>
        //        .TotalPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        //}
            
        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await  FindAll(trackChanges)
             .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
             .Search(bookParameters.SearchTerm)
             .Sort(bookParameters.OrderBy)
            .ToListAsync();

            return PagedList<Book>
                .TotalPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }



        public async Task<Book>  GetOneBookByIdAsync(int id, bool trackChanges) =>
           await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
         

        public void UpdateOneBook(Book book) => Update(book);
        
    }
}
