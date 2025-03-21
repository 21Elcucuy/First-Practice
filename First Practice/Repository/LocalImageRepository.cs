using First_Practice.Data;
using First_Practice.Models.Domain;

namespace First_Practice.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly DbContexts _dbContexts;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalImageRepository(DbContexts dbContexts,IWebHostEnvironment webHostEnvironment ,IHttpContextAccessor httpContextAccessor)
        {
            this._dbContexts = dbContexts;
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
            var LocalFilePath = Path.Combine(_webHostEnvironment.ContentRootPath ,"Images" , image.FileName + image.FileExtenstion);
            
            using var stream = new FileStream(LocalFilePath,FileMode.Create);
            await image.File.CopyToAsync(stream);

            var UrlPath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/images/{image.FileName}{image.FileExtenstion}";
            image.FilePath = UrlPath;
            await _dbContexts.Image.AddAsync(image);
            await _dbContexts.SaveChangesAsync();
            return image;


        }
    }
}
