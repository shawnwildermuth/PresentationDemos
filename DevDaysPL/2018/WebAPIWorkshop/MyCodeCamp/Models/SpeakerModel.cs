using System.Collections.Generic;

namespace MyCodeCamp.Models
{
  public class SpeakerModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string PhoneNumber { get; set; }
    public string WebsiteUrl { get; set; }
    public string TwitterName { get; set; }
    public string GitHubName { get; set; }
    public string Bio { get; set; }
    public string HeadShotUrl { get; set; }
    public ICollection<TalkModel> Talks { get; set; }
  }
}