﻿using Microsoft.AspNetCore.Identity;

namespace FreeHost.Infrastructure.Models.Authorization;

public class User : IdentityUser
{
    public string Login { get; set; }
    public byte[] Photo { get; set; }
}