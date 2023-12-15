namespace Infrastructure.Entities;

public class Metric : BaseEntity
{
    public string? Name { get; set; }
    public MetricType? MetricType { get; set; }
    public DateOnly FirstDate { get; set; }
    public DateOnly SecondDate { get; set; }
    public User? User { get; set; }
}