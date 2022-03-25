using Wedding.Data.Models;

namespace Wedding.Data;

public class Seeding
{
    private readonly IConfiguration _config;

    public Seeding(IConfiguration config) => _config = config;

    public List<Invitation> Seeds => _config
        .GetRequiredSection("Data")
        .Get<Seed[]>()
        .Select(x => new Invitation
        {
            Code = x.Code,
            Attendees = x.Names.Select(y => new Attendee { Name = y }).ToList(),
        })
        .ToList();

    private class Seed
    {
        public string Code { get; set; } = string.Empty;
        public List<string> Names { get; set; } = new();
    }
}
