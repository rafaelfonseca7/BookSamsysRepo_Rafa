using BookSamsysAPI.Models;
using BookSamsysAPI.DTOs;
using AutoMapper;

namespace BookSamsysAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<CreateBookDTO, Book>();

            CreateMap<Author, AuthorDTO>();

        }
    }
}
