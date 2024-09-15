﻿using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class BookRepoistory : RepositoryBase<Book>, IBookRepository
    {
        public BookRepoistory(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);
         
        public void DeleteOneBook(Book book) => Delete(book);
        

        public IQueryable<Book> GetAllBooks(bool trackChanges) => 
            FindAll(trackChanges)
            .OrderBy(x => x.Id); 


        public  Book  GetOneBookById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();
         

        public void UpdateOneBook(Book book) => Update(book);
        
    }
}
