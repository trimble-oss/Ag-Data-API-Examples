using System.ComponentModel.DataAnnotations;

namespace SampleCode.DataTransferObjects
{
    public class Prescription
    {
        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "The rate column value cannot exceed 10 characters.")]
        public string RateColumn { get; set; }
        
        public string? RateUnit { get; set; }

        public IList<Guid>? DeviceIds { get; set; }
    }
}
