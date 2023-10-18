namespace SampleCode.DataTransferObjects
{
    public class LoginToken
    {
        public string Token_Type { get; set; }
        public long Expires_In { get; set; }
        public string Access_Token { get; set; }
       
    }
}
