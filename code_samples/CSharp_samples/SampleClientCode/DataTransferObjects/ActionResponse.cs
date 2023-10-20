namespace SampleCode.DataTransferObjects
{
    public class ActionResponse<T>
    {
        public List<T> Result { get; set; }
        public Bookmark? Bookmark { get; set; }
    }
}
