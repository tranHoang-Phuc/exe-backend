namespace TuiToot.Server.Api.Dtos.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
