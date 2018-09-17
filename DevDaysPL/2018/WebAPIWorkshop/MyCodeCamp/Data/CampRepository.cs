using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Data
{
  public class CampRepository : ICampRepository
  {
    private CampContext _context;

    public CampRepository(CampContext context)
    {
      _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public IEnumerable<Camp> GetAllCamps()
    {
      return _context.Camps
                .Include(c => c.Location)
                .OrderBy(c => c.EventDate)
                .ToList();
    }

    public Camp GetCamp(int id)
    {
      return _context.Camps
        .Include(c => c.Location)
        .Where(c => c.Id == id)
        .FirstOrDefault();
    }

    public Camp GetCampWithSpeakers(int id)
    {
      return _context.Camps
        .Include(c => c.Location)
        .Include(c => c.Speakers)
        .ThenInclude(s => s.Talks)
        .Where(c => c.Id == id)
        .FirstOrDefault();
    }

    public Camp GetCampByMoniker(string moniker)
    {
      return _context.Camps
        .Include(c => c.Location)
        .Where(c => c.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
        .FirstOrDefault();
    }

    public Camp GetCampByMonikerWithSpeakers(string moniker)
    {
      return _context.Camps
        .Include(c => c.Location)
        .Include(c => c.Speakers)
        .ThenInclude(s => s.Talks)
        .Where(c => c.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
        .FirstOrDefault();
    }

    public Speaker GetSpeaker(int speakerId)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Where(s => s.Id == speakerId)
        .FirstOrDefault();
    }

    public IEnumerable<Speaker> GetSpeakers(int id)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Where(s => s.Camp.Id == id)
        .OrderBy(s => s.Name)
        .ToList();
    }

    public IEnumerable<Speaker> GetSpeakersWithTalks(int id)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Include(s => s.Talks)
        .Where(s => s.Camp.Id == id)
        .OrderBy(s => s.Name)
        .ToList();
    }

    public IEnumerable<Speaker> GetSpeakersByMoniker(string moniker)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Where(s => s.Camp.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
        .OrderBy(s => s.Name)
        .ToList();
    }

    public IEnumerable<Speaker> GetSpeakersByMonikerWithTalks(string moniker)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Include(s => s.Talks)
        .Where(s => s.Camp.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
        .OrderBy(s => s.Name)
        .ToList();
    }

    public Speaker GetSpeakerWithTalks(int speakerId)
    {
      return _context.Speakers
        .Include(s => s.Camp)
        .Include(s => s.Talks)
        .Where(s => s.Id == speakerId)
        .FirstOrDefault();
    }

    public Talk GetTalk(int talkId)
    {
      return _context.Talks
        .Include(t => t.Speaker)
        .ThenInclude(s => s.Camp)
        .Where(t => t.Id == talkId)
        .OrderBy(t => t.Title)
        .FirstOrDefault();
    }

    public IEnumerable<Talk> GetTalks(int speakerId)
    {
      return _context.Talks
        .Include(t => t.Speaker)
        .ThenInclude(s => s.Camp)
        .Where(t => t.Speaker.Id == speakerId)
        .OrderBy(t => t.Title)
        .ToList();
    }

    public CampUser GetUser(string userName)
    {
      return _context.Users
        .Where(u => u.UserName == userName)
        .Cast<CampUser>()
        .FirstOrDefault();

    }

    public async Task<bool> SaveAllAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}
