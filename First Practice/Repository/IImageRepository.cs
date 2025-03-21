using First_Practice.Models.Domain;

namespace First_Practice.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
