﻿using Microsoft.AspNetCore.Identity;

namespace DeliveryEat_vue1.Server.DataBase
{
    public class User
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
    }
}
