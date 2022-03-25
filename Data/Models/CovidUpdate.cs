using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Models;

public class CovidUpdate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Message { get; set; } = null!;  
}
