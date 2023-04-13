using Microsoft.AspNetCore.Mvc;
using WebAdvert.AdvertApi.Models;
using WebAdvert.AdvertApi.Services;

namespace WebAdvert.AdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertController : Controller
    {
        private readonly IAdvertStorageService _advertStorageService;
        private string _recordId;

        public AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            try
            {
                _recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException ex)
            {
                return new NotFoundResult() { };
            }
            catch (Exception ex1)
            {
                return StatusCode(500, ex1.Message);
            }

            return StatusCode(201, new CreateAdvertResponse { Id = _recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException ex)
            {
                return new NotFoundResult() { };
            }
            catch (Exception ex1)
            {
                return StatusCode(500, ex1.Message);
            }

            return new OkResult();
        }
    }
}
