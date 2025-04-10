using FeatureRequestPortal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace FeatureRequestPortal.MyFeatures
{
    [Route("api/app/my-feature")]
    public class MyFeatureController : FeatureRequestPortalController
    {
        private readonly IMyFeatureAppService _featureAppService;

        public MyFeatureController(IMyFeatureAppService featureAppService)
        {
            _featureAppService = featureAppService;
        }

        [HttpPost("update-score")]
        public async Task<IActionResult> UpdateScore([FromBody] UpdateFeatureScoreDto input)
        {
            var result = await _featureAppService.UpdateFeatureScoreAsync(input);

            if (result == null)
            {
                return BadRequest(new { message = "�zellik bulunamad� veya i�lem hatal�." });
            }

            return Ok(result);
        }

        [HttpGet("user-score")]
        public async Task<ActionResult<UserFeatureScoreDto>> GetUserFeatureScore(Guid featureId)
        {
            var userFeatureScore = await _featureAppService.GetUserFeatureScoreAsync(featureId);

            if (userFeatureScore == null)
            {
                return NotFound();
            }

            return Ok(userFeatureScore);
        }

        [HttpPost]
        [Route("approve")]
        [Authorize("FeatureRequestPortal.MyFeatures.Approve")]
        public Task ApproveAsync(UpdateFeatureApproveDto input)
        {
            return _featureAppService.ApproveFeatureAsync(input);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeatureDetailsAsync(Guid id)
        {
            try
            {
                var feature = await _featureAppService.GetFeatureDetailsAsync(id);
                return Ok(feature);
            }
            catch (UserFriendlyException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
