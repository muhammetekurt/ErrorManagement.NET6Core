﻿using ErrorManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ErrorManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Driver> Drivers { get; set; }
}