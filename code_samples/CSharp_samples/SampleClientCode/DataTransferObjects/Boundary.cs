
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace SampleCode.DataTransferObjects
{

    public class Boundary
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[^/\\:*<>|.""]+$", ErrorMessage = "Name contains one or more invalid characters(/\\:*<>|.\")")]
        public string Name { get; set; } = "";

        [Required(AllowEmptyStrings = false)]
        public Guid FieldId { get; set; }

        [Required]
        public Geometry Geometry { get; set; }
    }

}
