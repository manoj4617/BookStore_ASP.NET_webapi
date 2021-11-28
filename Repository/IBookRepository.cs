using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;


namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<List<BooksModel>> GetAllBooks();
        Task<BooksModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BooksModel bookModel);
        Task UpdateBookAsync(int id, BooksModel toUpdateBook);
        Task UpdateBookPatchAsync(int id, JsonPatchDocument toUpdateBook);
        Task DeleteBookAsync(int id);

    }
}
