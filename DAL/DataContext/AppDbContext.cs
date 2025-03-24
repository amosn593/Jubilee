using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
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
}
