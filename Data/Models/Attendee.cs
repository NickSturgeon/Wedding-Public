using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Models;

public class Attendee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public bool? RSVP { get; set; }

    public bool VegetarianOption { get; set; }

    public string? DietaryRestrictions { get; set; }


    public int InvitationId { get; set; }

    public virtual Invitation Invitation { get; set; } = null!;
}
