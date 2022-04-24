using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using Mosaik.idAPI.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Services
{
    public class MosaikHistoryRepository : IMosaikHistoryRepository
    {
        private readonly IDataContext _context;

        public class IdDto
        {
            public int Id {get; set;}
        }
        public class ResultDto
        {
            public string Link {get; set;}
            public string Time {get; set;}
        }
        public MosaikHistoryRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MosaikHistory>> getAll()
        {
            return await _context.MosaikHistories.ToListAsync();
        }

        public async Task<String> InsertHistory(string Email, string Link, string Time, string Date)
        {
            var mosaikChildren = await _context.MosaikChildren.ToListAsync();
            foreach (var mosaikChild in mosaikChildren)
            {
                if (mosaikChild.Email == Email)
                {
                    var mosaikUsers = await _context.MosaikUsers.ToListAsync();
                    foreach (var mosaikUser in mosaikUsers)
                    {
                        if (mosaikUser.Email == Email)
                        {
                            MosaikDateHistory mosaikDateHistory = new()
                            {
                                userID = mosaikUser.MosaikUserID,
                                Date = Date,
                            };
                            _context.MosaikDateHistories.Add(mosaikDateHistory);
                            await _context.SaveChangesAsync();
                            MosaikHistory mosaikHistory = new()
                            {
                                userID = mosaikUser.MosaikUserID,
                                Link = Link,
                                AccessedTime = Time,
                                MosaikDateHistoryID = mosaikDateHistory.MosaikDateHistoryID
                            };
                            _context.MosaikHistories.Add(mosaikHistory);
                            await _context.SaveChangesAsync();
                            return "success";
                        }
                    }
                    return "failed";
                }
            }
            var mosaikParents = await _context.MosaikParents.ToListAsync();
            foreach (var mosaikParent in mosaikParents)
            {
                if (mosaikParent.Email == Email)
                {
                    var mosaikUsers = await _context.MosaikUsers.ToListAsync();
                    foreach (var mosaikUser in mosaikUsers)
                    {
                        if (mosaikUser.Email == Email)
                        {
                            MosaikDateHistory mosaikDateHistory = new()
                            {
                                userID = mosaikUser.MosaikUserID,
                                Date = DateTime.Now.ToString("d"),
                            };
                            _context.MosaikDateHistories.Add(mosaikDateHistory);
                            await _context.SaveChangesAsync();
                            MosaikHistory mosaikHistory = new()
                            {
                                userID = mosaikUser.MosaikUserID,
                                Link = Link,
                                AccessedTime = DateTime.Now.ToString("t"),
                                MosaikDateHistoryID = mosaikDateHistory.MosaikDateHistoryID
                            };
                            _context.MosaikHistories.Add(mosaikHistory);
                            await _context.SaveChangesAsync();
                            return "success";
                        }
                    }
                    return "failed";
                }
            }
            return "failed";
        }
        public async Task<int> GetTotalDateHistory(string Email)
        {
            var users = await _context.MosaikUsers.ToListAsync();
            foreach (var user in users)
            {
                if (user.Email == Email)
                {
                    int count = _context.MosaikDateHistories.Where(o => o.userID == user.MosaikUserID).Select(o => new {o.Date}).Distinct().Count();
                    return count;
                }
            }
            return -1;
        }
        public async Task<List<String>> GetListDateHistory(string Email)
        {
            var users = await _context.MosaikUsers.ToListAsync();
            List<String> result = new List<string>();
            foreach (var user in users)
            {
                if (user.Email == Email)
                {
                    var list = _context.MosaikDateHistories.Where(o => o.userID == user.MosaikUserID).Select(o => new {o.Date}).Distinct().ToList();
                    
                    foreach (var item in list)
                    {
                        result.Add(item.Date);
                    }
                }
            }
            return result;
        }
        public async Task<int> GetTotalHistoryPerDate(string Email, string Date)
        {
            var users = await _context.MosaikUsers.ToListAsync();
            foreach (var user in users)
            {
                if (user.Email == Email)
                {
                    int count = _context.MosaikDateHistories.Where(o => o.userID == user.MosaikUserID && o.Date == Date).Count();
                    return count;
                }
            }
            return -1;
        }
        public async Task<LinkAndTimes> GetListDateHistoryPerDate(string Email, string Date)
        {
            var users = await _context.MosaikUsers.ToListAsync();
            List<String> links = new List<string>();
            List<String> times = new List<string>();
            foreach (var user in users)
            {
                if (user.Email == Email)
                {
                    List<IdDto> list = _context.MosaikDateHistories.Where(o => o.userID == user.MosaikUserID && o.Date == Date).Select(o => new IdDto {Id = o.MosaikDateHistoryID}).ToList();
                    var MosaikHistories = await _context.MosaikHistories.ToListAsync();
                    if (list.Count() > 0)
                    {
                        foreach (IdDto ID in list)
                        {
                            foreach (var mosaikHistory in MosaikHistories)
                            {
                                if (mosaikHistory.MosaikDateHistoryID == ID.Id)
                                {
                                    links.Add(mosaikHistory.Link);
                                    times.Add(mosaikHistory.AccessedTime);
                                }
                            }
                        }
                    }
                }
            }
            LinkAndTimes linkAndTimes = new()
            {
                Links = links,
                Times = times
            };
            return linkAndTimes;
        }
    }
}
