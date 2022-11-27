using System.ComponentModel.DataAnnotations;

namespace SensorState.Models;

public class Temperature
{
    [Key]
    public int Id { get; set; }

    public int Value { get; set; }

    public int StatusId { get; set; }

    public virtual Status Status { get; set; } = null!;
}

