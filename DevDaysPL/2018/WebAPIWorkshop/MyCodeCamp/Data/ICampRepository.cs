using System.Collections.Generic;
using System.Threading.Tasks;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Data
{
  public interface ICampRepository
  {
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    IEnumerable<Camp> GetAllCamps();
    Camp GetCamp(int id);
    Camp GetCampByMoniker(string moniker);
    Camp GetCampByMonikerWithSpeakers(string moniker);
    Camp GetCampWithSpeakers(int id);
    Speaker GetSpeaker(int speakerId);
    IEnumerable<Speaker> GetSpeakers(int id);
    IEnumerable<Speaker> GetSpeakersByMoniker(string moniker);
    IEnumerable<Speaker> GetSpeakersByMonikerWithTalks(string moniker);
    IEnumerable<Speaker> GetSpeakersWithTalks(int id);
    Speaker GetSpeakerWithTalks(int speakerId);
    Talk GetTalk(int talkId);
    IEnumerable<Talk> GetTalks(int speakerId);
    CampUser GetUser(string userName);
    Task<bool> SaveAllAsync();
  }
}