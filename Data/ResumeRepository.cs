using Microsoft.AspNetCore.Identity;
using MRDN68_SOF_2022231.Controllers;
using MRDN68_SOF_2022231.Models;
using System.Security.Claims;

namespace MRDN68_SOF_2022231.Data
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public ResumeRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        public void Create(Resume resume)
        {
            var resumeSameName = _context.Resumes.FirstOrDefault(t => t.FirstName == resume.FirstName && t.LastName == resume.LastName);
            if (resumeSameName != null)
            {
                throw new ArgumentException("Resume with this name already exists");
            }
            _context.Add(resume);
            _context.SaveChanges();
        }

        public IEnumerable<Resume> Read()
        {

            return _context.Resumes;
        }

        //public Resume? Read(string name)
        //{
        //    return context.Resumes.FirstOrDefault(t => t.Name == name);
        //}

        public IEnumerable<Resume> ReadFromOwnerId(ClaimsPrincipal principal)
        {
            var uid = _userManager.GetUserId(principal);
            if (uid == null)
            {
                throw new ArgumentException("User connot be found!");
            }

            return _context.Resumes.Where(t => t.OwnerId == uid);

        }

        public Resume ReadOneById(string id)
        {
            return _context.Resumes.First(x => x.Id == id);
        }

        public void Delete(string id, ClaimsPrincipal principal)
        {
            var item = _context.Resumes.FirstOrDefault(t => t.Id == id);
            if (item != null && item.OwnerId == _userManager.GetUserId(principal))
            {
                _context.Resumes.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User cannot be found!");
            }

            //var hero = Read(name);
            //_context.Heroes.Remove(hero);
            //_context.SaveChanges();
        }

        public void Update(Resume resume)
        {
            //var old = Read(hero.Name);
            //old.Alien = hero.Alien;
            //old.Power = hero.Power;
            //old.Side = hero.Side;
            //context.SaveChanges();
        }

    }
}
