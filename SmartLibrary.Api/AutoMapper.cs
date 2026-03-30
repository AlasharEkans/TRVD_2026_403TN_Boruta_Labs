using AutoMapper;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.DTOs;

namespace SmartLibrary.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
    }
}