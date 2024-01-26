namespace MvcMovie.Models;

public class GeoTask
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public string Note { get; set; } = "";

    public string Status {get; set;} = "В рассмотрении";

    public int Group {get; set;} = 0;
    public DateOnly DayBegin {get; set;}
    public DateOnly DayEnd {get; set;}    
    
}   