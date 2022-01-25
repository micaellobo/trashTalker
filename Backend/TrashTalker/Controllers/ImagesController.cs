using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrashTalker.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImagesController(IWebHostEnvironment _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }

        //View Image
        [HttpGet("{imageId:guid}")]
        public IActionResult getImage(Guid imageId)
        {
            try
            {
                var image = System.IO.File.ReadAllBytes(
                    $"{_hostEnvironment.ContentRootPath}/images/{imageId}.jpeg"); // You can use your own method over here.
                return File(image, $"{imageId}/jpeg",imageId + ".jpeg");
            }
            catch (FileNotFoundException)
            {
                return NotFound("Image does not exist");
            }
        }


        //Upload Image
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file, Guid id)
        {
            if (file is null)
                return BadRequest("Missing the image!");

            if (file.Length <= 0)
                return BadRequest("Missing the image!");
            await using var stream =
                System.IO.File.Create($"{_hostEnvironment.ContentRootPath}/images/{id}.jpeg");
            await file.CopyToAsync(stream);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}