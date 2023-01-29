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

        //public Resume? Read(string name)
        //{
        //    return context.Resumes.FirstOrDefault(t => t.Name == name);
        //}

        public IEnumerable<Workplace> ReadFromId(string uid)
        {
            return _context.Workplaces.Where(t => t.OwnerId == uid);
        }

    }
}
