using AutoMapper;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookRequest, Book>().ReverseMap();
            CreateMap<Book, BookResponse>();

            CreateMap<ReaderRequest, Reader>().ReverseMap();
            CreateMap<Reader, ReaderResponse>();
        }
    }
}
