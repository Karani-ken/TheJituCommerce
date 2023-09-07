﻿using Microsoft.EntityFrameworkCore;
using TheJitu_EmailService.Models;

namespace TheJitu_EmailService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
