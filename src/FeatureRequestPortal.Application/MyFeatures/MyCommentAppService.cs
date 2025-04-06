using FeatureRequestPortal.MyFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Volo.Abp;
using Volo.Abp.Identity;
using System.Linq;
using Volo.Abp.Authorization;
using FeatureRequestPortal.Permissions;

public class CommentAppService : ApplicationService, IMyCommentAppService
{
    private readonly IRepository<MyComment, Guid> _commentRepository;
    private readonly IRepository<MyFeature, Guid> _featureRequestRepository;
    private readonly IRepository<IdentityUser, Guid> _userRepository;
    private readonly ICurrentUser _currentUser;

    public CommentAppService(
        IRepository<MyComment, Guid> commentRepository,
        IRepository<MyFeature, Guid> featureRequestRepository,
        IRepository<IdentityUser, Guid> userRepository, 
        ICurrentUser currentUser)
    {
        _commentRepository = commentRepository;
        _featureRequestRepository = featureRequestRepository;
        _userRepository = userRepository;
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<MyCommentDto> CreateAsync(CreateMyCommentDto input)
    {
        if (!_currentUser.IsAuthenticated || !_currentUser.Id.HasValue)
        {
            throw new AbpAuthorizationException("Giriş yapmanız gerekmektedir.");
        }

        var featureRequest = await _featureRequestRepository.FirstOrDefaultAsync(x => x.Id == input.FeatureRequestId);
        if (featureRequest == null)
        {
            throw new UserFriendlyException("Özellik talebi bulunamadı.");
        }

        var comment = new MyComment
        {
            FeatureRequestId = input.FeatureRequestId,
            UserId = _currentUser.Id.Value,
            Content = input.Content,
            CreationTime = DateTime.Now
        };

        await _commentRepository.InsertAsync(comment);

        var commentDto = ObjectMapper.Map<MyComment, MyCommentDto>(comment);

        return commentDto;
    }

    public async Task<List<MyCommentDto>> GetCommentsByFeatureRequestIdAsync(Guid featureRequestId)
    {
        var comments = await _commentRepository.GetListAsync(x => x.FeatureRequestId == featureRequestId);

        if (!comments.Any())
        {
            return new List<MyCommentDto>();
        }

        var sortedComments = comments
            .OrderBy(x => x.CreationTime)
            .ToList();

        var result = new List<MyCommentDto>();

        foreach (var comment in sortedComments)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == comment.UserId);

            result.Add(new MyCommentDto
            {
                Id = comment.Id,
                FeatureRequestId = comment.FeatureRequestId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreationTime = comment.CreationTime
            });
        }

        return result;
    }
    public async Task<MyCommentDto> UpdateAsync(Guid id, UpdateMyCommentDto input)
    {
        var comment = await _commentRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (comment == null)
        {
            throw new UserFriendlyException("Yorum bulunamadı.");
        }

        comment.Content = input.Content;
        await _commentRepository.UpdateAsync(comment);

        var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == comment.UserId);
        return new MyCommentDto
        {
            Id = comment.Id,
            FeatureRequestId = comment.FeatureRequestId,
            UserId = comment.UserId,
            Content = comment.Content,
            CreationTime = comment.CreationTime
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var comment = await _commentRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (comment == null)
        {
            throw new UserFriendlyException("Yorum bulunamadı.");
        }

        await _commentRepository.DeleteAsync(comment);
    }
}
