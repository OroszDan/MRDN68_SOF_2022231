using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        IResumeRepository ResumeRepo;
        IWorkplaceRepository WorkplaceRepo;

        public AdminController(IResumeRepository resumeRepo, IWorkplaceRepository workplaceRepo)
        {
            ResumeRepo = resumeRepo;
            WorkplaceRepo = workplaceRepo;
        }

        public IActionResult List()
        {
            IEnumerable<Resume> resumes = ResumeRepo.Read();
            return View(resumes);
        }

        public IActionResult ListWorkplaces(string resumeId)
        {
            IEnumerable<Workplace> workplaces = WorkplaceRepo.ReadFromUid(resumeId);
            return View(workplaces);
        }


        public IActionResult Update(string id)
        {
            var result = ResumeRepo.ReadOneById(id);
            return View(result);
        }

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

        public IActionResult UpdateWorkplace(string id)
        {
            var workplace = WorkplaceRepo.ReadOneById(id); 
            return View(workplace);
        }

        [HttpPost]
        public IActionResult UpdateWorkplace(Workplace workplace)
        {
            if (!ModelState.IsValid)
            {
                return View(workplace);
            }
            WorkplaceRepo.Update(workplace);
            return RedirectToAction(nameof(ListWorkplaces), new { resumeId = workplace.OwnerId});
        }

        public IActionResult Statistics()
        {
            var p1 = WorkplaceRepo.LongestWorkingTime();
            var p2 = ResumeRepo.YoungestPerson();
            Stat? stat = new Stat();
            if (p1 != null && p2 != null)
            {
               var res1 = ResumeRepo.ReadOneById(p1.OwnerId);
                stat.EFirstName = res1.FirstName;
                stat.ELastName = res1.LastName;
                stat.EYears = p1.WorkedYears; // not great
                stat.YFirstName = p2.FirstName;
                stat.YLastName = p2.LastName;
                stat.YAge = p2.Age;
            }                  
            
            return View(stat);
        }

        public IActionResult Delete(string id)
        {
            var item = ResumeRepo.ReadOneById(id);
            if (item != null)
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
    }
}
