using FeatureRequestPortal.MyFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;

[AbpAutoValidateAntiforgeryToken]
[Route("api/app/comments")]
public class CommentController : AbpController
{
    private readonly IMyCommentAppService _commentAppService;

    public CommentController(IMyCommentAppService commentAppService)
    {
        _commentAppService = commentAppService;
    }

    [HttpGet("{featureRequestId}")]
    public async Task<List<MyCommentDto>> GetCommentsByFeatureRequestIdAsync(Guid featureRequestId)
    {
        return await _commentAppService.GetCommentsByFeatureRequestIdAsync(featureRequestId);
    }

    [HttpPost]
    [Authorize]
    public async Task<MyCommentDto> CreateAsync([FromBody] CreateMyCommentDto input)
    {
        return await _commentAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task<MyCommentDto> UpdateAsync(Guid id, UpdateMyCommentDto input)
    {
        return await _commentAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _commentAppService.DeleteAsync(id);
    }
}
