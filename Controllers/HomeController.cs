using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;
using NuGet.Packaging;
using System.Diagnostics;

namespace MRDN68_SOF_2022231.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        IResumeRepository ResumeRepo;
        IWorkplaceRepository WorkplaceRepo;

        public HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext db, IResumeRepository resumeRepo, IWorkplaceRepository workplaceRepo)
        {
            _userManager = userManager;
            _db = db;
            ResumeRepo = resumeRepo;
            WorkplaceRepo = workplaceRepo;
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
            if (!ModelState.IsValid)
            {
                return View(resume);
            }
            
            ResumeRepo.Create(resume);
            return RedirectToAction(nameof(AddWorkplace), new { ownerId = resume.Id });

        }

        [Authorize]
        public IActionResult AddWorkplace(string id)
        {
            Workplace newWorkplace = new Workplace { OwnerId = id };
            return View(newWorkplace);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddWorkplace(Workplace workplace) 
        {
            if (!ModelState.IsValid)
            {
                return View(workplace);
            }
            WorkplaceRepo.Create(workplace);
            Workplace newWorkplace = new Workplace { OwnerId = workplace.OwnerId };
            return View(newWorkplace);
        }

        [Authorize]
        public IActionResult List()
        {
            IEnumerable<Resume> resume = ResumeRepo.ReadFromOwnerId(this.User);
            return View(resume);
        }

        [Authorize]
        public IActionResult ListWorkplaces(string resumeId) 
        {
            IEnumerable<Workplace> workplaces = WorkplaceRepo.ReadFromUid(resumeId);
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

        [Authorize]
        public IActionResult Delete(string id)
        {
            var item = ResumeRepo.ReadOneById(id);
            if (item != null && item.OwnerId == _userManager.GetUserId(this.User))
            {
                ResumeRepo.Delete(item);
            }
            else
            {
                throw new ArgumentException("Something went wrong!");
            }
            
            return RedirectToAction(nameof(List));
        }

        public IActionResult DeleteWorkplace(string id)
        {
            WorkplaceRepo.Delete(id);

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