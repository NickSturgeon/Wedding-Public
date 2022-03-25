using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Models;

public class Invitation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Code { get; set; } = null!;

    public bool AccomodationsNeeded { get; set; }

    public string? GeneralComments { get; set; }

    public bool CovidAgreement { get; set; }

    public string? Email { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int SubmissionCount { get; set; }

    public bool HasKids { get; set; }


    public virtual List<Attendee> Attendees { get; set; } = new();
}
