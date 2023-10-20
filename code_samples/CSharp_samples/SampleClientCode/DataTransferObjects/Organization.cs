
namespace SampleCode.DataTransferObjects
{
    public class Organization : LinkBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string TimezoneName { get; set; } = "UTC";

        public string CurrencyCode { get; set; } = "USD";

        public bool IsMetric { get; set; }
    }
}
