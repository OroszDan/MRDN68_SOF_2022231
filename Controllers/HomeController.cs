using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;
using NuGet.Packaging;
using System.Diagnostics;

namespace MRDN68_SOF_2022231.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        IResumeRepository ResumeRepo;
        IWorkplaceRepository WorkplaceRepo;

        public HomeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger, ApplicationDbContext db, IResumeRepository resumeRepo, IWorkplaceRepository workplaceRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _db = db;
            ResumeRepo = resumeRepo;
            WorkplaceRepo = workplaceRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Resume resume) 
        {
            resume.OwnerId = _userManager.GetUserId(this.User);
            ResumeRepo.Create(resume);
            return RedirectToAction("AddWorkplace", new { ownerId = resume.Id });

        }

        [Authorize]
        public IActionResult AddWorkplace(string ownerId)
        {
            Workplace newWorkplace = new Workplace { OwnerId = ownerId };
            return View(newWorkplace);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddWorkplace(Workplace workplace) 
        {
            WorkplaceRepo.Create(workplace);
            Workplace newWorkplace = new Workplace { OwnerId = workplace.OwnerId };
            return View(newWorkplace);
        }

        [Authorize]
        public IActionResult List()
        {
            //if (!ModelState.IsValid)
            //{

            //}
            IEnumerable<Resume> resume = ResumeRepo.ReadFromOwnerId(this.User);
            return View(resume);
        }

        [Authorize]
        public IActionResult ListWorkplaces(string resumeId) 
        {
            IEnumerable<Workplace> workplaces = WorkplaceRepo.ReadFromId(resumeId);
            return View(workplaces);
        }

        [Authorize]
        public IActionResult Update(string id)
        {
            var result = ResumeRepo.ReadOneById(id);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return View(resume);
            }
            ResumeRepo.Update(resume);
            return RedirectToAction(nameof(List));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}