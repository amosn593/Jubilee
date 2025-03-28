﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<AccessTokenResponse> AccessTokenResponses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<SMSConfigModel> SMSConfigModels { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Branch> Branches { get; set; }
}
