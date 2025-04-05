using FeatureRequestPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
                return BadRequest(new { message = "Özellik bulunamadý veya iþlem hatalý." });
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
    }
}
