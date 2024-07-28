using AutoMapper;
using HomeCompassApi.Entities.Feed;
using HomeCompassApi.Models.Feed.Post;

namespace HomeCompassApi.Profiles.Feed;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<UpdatePostDTO, Post>();
        CreateMap<Post, ReadPostDTO>()
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));
        
        CreateMap<Post, ReadAllPostsDTO>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.AuthorPhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));
    }
}