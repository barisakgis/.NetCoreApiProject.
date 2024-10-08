﻿using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        private readonly IMapper  _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
           _mapper = mapper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
           
             var entity = _mapper.Map<Book>(bookDto);
           _manager.Book.CreateOneBook(entity);
           await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        { 
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();

        }

        public async Task<(IEnumerable<BookDto> books, MetaData MetaData)> GetAllBooksAsync(BookParameters bookParameters , bool trackChanges)
        {
            if(!bookParameters.ValidPriceRange)
            {
                throw new PriceOutoRangeBadRequestException();
            }

            var booksWithMetaData = await _manager.Book.GetAllBooksAsync(bookParameters, trackChanges);
            var booksDto =  _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            return (booksDto, booksWithMetaData.MetaData);
        }

        public async Task< BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
             var book  = await GetOneBookByIdAndCheckExists(id,trackChanges);

            return _mapper.Map<BookDto>(book);

        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto,bool trackChanges)
        {

            //if (entity is null)
            //{
            //    string msg = $"book with id {id} could not found.";
            //    _logger.LogInfo(msg);
            //    throw new Exception(msg);

            //}
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
             
            //mapping
            //entity.Title = book.Title;  
            //entity.Price = book.Price;

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists ( int id,bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }

            return entity;
        }
    } 
}
