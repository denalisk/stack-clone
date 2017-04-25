﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StackApp.Models
{
    public class StackAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public StackAppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
