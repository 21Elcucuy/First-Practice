namespace First_Practice.Models.DTO
{
    public class ImageDTO
    {
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }
        public string FileExtenstion { get; set; }
        public long FileSizeInByte { get; set; }
        public string FilePath { get; set; }
    }
}
