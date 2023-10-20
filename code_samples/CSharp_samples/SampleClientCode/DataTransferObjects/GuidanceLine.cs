
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace SampleCode.DataTransferObjects
{
    public class GuidanceLine
    {
        public enum PatternType
        {
            AB,
            Curve,
            Headland,
        }

        public Guid FieldId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[^/\\:*<>|.""]+$", ErrorMessage = "Name contains one or more invalid characters(/\\:*<>|.\")")]
        public string Name { get; set; }

        [Required()]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PatternType Type { get; set; }

        public Geometry? Origin { get; set; }

        public Guid? InfillId { get; set; }

        public Geometry Geometry { get; set; }

        public int? NumberOfSwaths { get; set; }
    }

}
