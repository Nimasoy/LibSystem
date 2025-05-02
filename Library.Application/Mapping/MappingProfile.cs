using AutoMapper;
using Library.Application.Features.Books.Queries.GetBookDetails;
using Library.Application.Features.Users.Queries.GetUserBorrowedBooks;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDetailsDto>()
            // Map Category from the first category in the Categories collection
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Categories.FirstOrDefault() != null ? src.Categories.FirstOrDefault().Name.Value : string.Empty))
            // Map Tags by selecting the Value property from each TagName
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name.Value).ToList()));

        CreateMap<BorrowRecord, BorrowedBookDto>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.Book.ISBN));
    }
} 