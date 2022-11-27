using Microsoft.EntityFrameworkCore;
using SensorState.Models;

namespace SensorState.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<Status> Statuses { get; set; }
    public DbSet<StatusBound> StatusBounds { get; set; }
    public DbSet<Temperature> Temperatures { get; set; }
}
