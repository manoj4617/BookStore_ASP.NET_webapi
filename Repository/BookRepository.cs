using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _bookContext;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext bookContext, IMapper mapper)
        {
            this._bookContext = bookContext;
            this._mapper = mapper;
        }

        public async Task<List<BooksModel>> GetAllBooks()
        {
            //Code without using AutoMapper
            /* var books = await _bookContext.Books.Select(x => new BooksModel()
             {
                 Id = x.Id,
                 Title = x.Title,
                 Description = x.Description
             }).ToListAsync();

             return books;*/

            //Code using AutpoMapper
            return _mapper.Map<List<BooksModel>>(await _bookContext.Books.ToListAsync());
        }

        public async Task<BooksModel> GetBookByIdAsync(int id)
        {
            //Code without using AutoMapper
            /*var book = await _bookContext.Books.Where(x => x.Id == id).Select(x => new BooksModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();
            return book;*/

            //Code using AutoMapper
            var book = await _bookContext.Books.FindAsync(id);
            return _mapper.Map<BooksModel>(book);
        }

        public async Task<int> AddBookAsync(BooksModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BooksModel toUpdateBook)
        {
            /*var book = await _bookContext.Books.FindAsync(id);
            if(book != null)
            {
                book.Title = toUpdateBook.Title;
                book.Description = toUpdateBook.Description;
                 _bookContext.SaveChanges();
            }*/

            var book = new Books()
            {
                Id = id,
                Title = toUpdateBook.Title,
                Description = toUpdateBook.Description
            };

            _bookContext.Books.Update(book);
            await _bookContext.SaveChangesAsync();
        }

        public async Task UpdateBookPatchAsync(int id, JsonPatchDocument toUpdateBook)
        {
            var book = await _bookContext.Books.FindAsync(id);
            if(book != null)
            {
                toUpdateBook.ApplyTo(book);
                await _bookContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new Books() { Id = id};

           var deletedbook =  _bookContext.Remove(book);
            await _bookContext.SaveChangesAsync();

        }
    }
}
