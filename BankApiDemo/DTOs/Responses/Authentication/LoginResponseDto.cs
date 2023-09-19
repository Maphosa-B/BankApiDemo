namespace BankApiDemo.DTOs.Responses.Authentication
{
    public class LoginResponseDto
    {
        public int Status { get; set; }
        public string Token { get; set; } = "";
        public  List<string>  ErrorsList { get; set; } = new List<string>();
    }
}
