using System.ComponentModel.DataAnnotations.Schema;

namespace First_Practice.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }
        public string FileExtenstion { get; set; }
        public long FileSizeInByte { get; set; }
        public string FilePath { get; set; }
    }
}
