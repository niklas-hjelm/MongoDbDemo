using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class HumanContext :DbContext
{
    public DbSet<Human> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=LAPTOP-94UMSFPO;Initial Catalog=HumanityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        base.OnConfiguring(optionsBuilder);
    }
}