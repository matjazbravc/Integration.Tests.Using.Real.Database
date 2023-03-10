using IntegrationTestingWithDockerDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTestingWithDockerDemo.Data;

public class MySqlDbContext : DbContext
{
    public DbSet<Student>? Students { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
        : base(options)
    {
    }
}