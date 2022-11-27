using System.ComponentModel.DataAnnotations;

namespace SensorState.Models;

public class Status
{
    [Key]
    public int StatusId { get; set; }

    public string Description { get; set; } = null!;

    public int StatusBoundId { get; set; }

    public virtual StatusBound StatusBound { get; set; } = null!;

    public virtual ICollection<Temperature> Temperatures { get; } = new List<Temperature>();
}
