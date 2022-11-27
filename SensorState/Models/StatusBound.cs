using System.ComponentModel.DataAnnotations;

namespace SensorState.Models;

public class StatusBound
{
    [Key]
    public int StatusBoundId { get; set; }

    public int UpperColdBound { get; set; }

    public int LowerHotBound { get; set; }

    public virtual ICollection<Status> Temperatures { get; } = new List<Status>();
}
