﻿using FluentValidation;

namespace Mates.Core.DTO.UserDTOs
{
    public class UserCreateRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
    }
}
