using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MRDN68_SOF_2022231.Models;
using System.Security.Claims;

namespace MRDN68_SOF_2022231.Data
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkplaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Workplace workplace)
        {
            _context.Add(workplace);
            _context.SaveChanges();
        }

        public IEnumerable<Workplace> Read()
        {
            return _context.Workplaces;
        }

        public IEnumerable<Workplace> ReadFromResumeId(string resumeId)
        {
            return _context.Workplaces.Where(t => t.OwnerId == resumeId);
        }

        public Workplace? ReadOneById(string id)
        {
            return _context.Workplaces.FirstOrDefault(x => x.Id == id);
        }

        public Workplace? LongestWorkingTime()
        {
            return _context.Workplaces.FirstOrDefault(x => x.WorkedYears == _context.Workplaces.Max(x => x.WorkedYears));
        }

        public void Update(Workplace workplace)
        {
            var old = ReadOneById(workplace.Id);
            old.CompanyName = workplace.CompanyName;
            old.City = workplace.City;
            old.WorkedYears = workplace.WorkedYears;
            old.Role = workplace.Role;
            _context.SaveChanges();
        }

        public void Delete(string id)
        { 
            var item = _context.Workplaces.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                _context.Workplaces.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Something went wrong!");
            }
        }

    }
}
