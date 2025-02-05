namespace TuiToot.Server.Api.Dtos.Request
{
    public class UpdatedProfileRequest
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}
