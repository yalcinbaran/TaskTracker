﻿namespace TaskTracker.SharedKernel.Common
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }

        public UserDTO() { }
    }
}
