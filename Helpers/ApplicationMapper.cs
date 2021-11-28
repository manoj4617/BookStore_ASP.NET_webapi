using AutoMapper;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //Mapping data of Books class to BooksModel class and also reverse mapping
            CreateMap<Books, BooksModel>().ReverseMap();
        }
    }
}
