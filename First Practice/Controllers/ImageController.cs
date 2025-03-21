using First_Practice.Models.Domain;
using First_Practice.Models.DTO;
using First_Practice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository) 
        {
            this._imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageRequestDTO imageRequestDTO)
        {
            ValidateImage(imageRequestDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageDomain = new Image
            {
                File = imageRequestDTO.File,
                FileName = imageRequestDTO.FileName,
                FileDescription = imageRequestDTO.FileDescription,
                FileExtenstion = Path.GetExtension(imageRequestDTO.File.FileName),
                FileSizeInByte = imageRequestDTO.File.Length,
            };
            imageDomain = await _imageRepository.Upload(imageDomain);

            var imageDTO = new ImageDTO
            {
                File = imageDomain.File,
                FileName = imageDomain.FileName,
                FileDescription = imageDomain.FileDescription,
                FileExtenstion = imageDomain.FileExtenstion,
                FilePath = imageDomain.FilePath,
                FileSizeInByte = imageDomain.FileSizeInByte,
            };
            return Ok(imageDTO);

        }
        private void ValidateImage(ImageRequestDTO imageRequestDTO)
        {
            var AllowExtension = new string[] { "png" , "jpeg" , "jpg" };

            if (AllowExtension.Contains(Path.GetExtension(imageRequestDTO.File.FileName)))
                ModelState.AddModelError("File" , "Unsupported file extenstion");
          if(imageRequestDTO.File.Length > 15728640)
                ModelState.AddModelError("File", "File Size More than 15 MB");



        }
    }
}
