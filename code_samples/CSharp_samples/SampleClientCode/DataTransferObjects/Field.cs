
using System.ComponentModel.DataAnnotations;

namespace SampleCode.DataTransferObjects
{
    public class Field 
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Name value cannot exceed 50 characters.")]
        [RegularExpression(@"^[^/\\:*<>|.""]+$", ErrorMessage = "Name contains one or more invalid characters(/\\:*<>|.\")")]
        public string Name { get; set; } = "";
        public Guid? FarmId { get; set; }

        //Area in hectares
        public double Area { get; set; }
    }
}
