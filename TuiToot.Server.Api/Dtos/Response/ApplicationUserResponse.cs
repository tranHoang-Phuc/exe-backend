﻿namespace TuiToot.Server.Api.Dtos.Response
{
    public class ApplicationUserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
