using AutoMapper;
using FeatureRequestPortal.MyFeatures;

namespace FeatureRequestPortal.Web;

public class FeatureRequestPortalWebAutoMapperProfile : Profile
{
    public FeatureRequestPortalWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<MyFeatureDto, CreateMyFeatureDto>();
        CreateMap<MyFeatureDto, UpdateMyFeatureDto>();

        CreateMap<MyComment, MyCommentDto>();
        CreateMap<CreateMyCommentDto, MyComment>();
    }
}



