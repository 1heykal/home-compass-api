using AutoMapper;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.Feed;

namespace HomeCompassApi.Profiles.Feed;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.AuthorPhotoURL, opt => opt.MapFrom(src => src.User.PhotoUrl));
    }
}