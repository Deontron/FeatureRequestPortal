using AutoMapper;
using FeatureRequestPortal.MyFeatures;

namespace FeatureRequestPortal;

public class FeatureRequestPortalApplicationAutoMapperProfile : Profile
{
    public FeatureRequestPortalApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<MyFeature, MyFeatureDto>();
        CreateMap<CreateMyFeatureDto, MyFeature>();
        CreateMap<UpdateMyFeatureDto, MyFeature>();

        CreateMap<MyComment, MyCommentDto>();
        CreateMap<CreateMyCommentDto, MyComment>();
        CreateMap<UpdateMyCommentDto, MyComment>();
    }
}
