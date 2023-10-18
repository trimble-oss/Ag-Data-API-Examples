namespace SampleCode.DataTransferObjects
{
    public class LoginToken
    {
        public string TokenType { get; set; }
        public long ExpiresIn { get; set; }
        public string AccessToken { get; set; }
       
    }
}
