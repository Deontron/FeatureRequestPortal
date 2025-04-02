using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FeatureRequestPortal.MyFeatures
{
    public interface IMyFeatureAppService :
    ICrudAppService< //Defines CRUD methods
        MyFeatureDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMyFeatureDto> //Used to create/update a book
    {
    }
}
