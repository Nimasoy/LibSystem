using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.DTOs.Book;
using Library.Domain.Entities;

namespace Library.Application.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                // Map CategoryName from the first category in the Categories collection
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.FirstOrDefault() != null ? src.Categories.FirstOrDefault().Name.Value : null));

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.TotalCopies));

            CreateMap<UpdateBookDto, Book>();
        }
    }
}

