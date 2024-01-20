using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StatisticsController : ControllerBase
    {
        readonly IWorkplaceRepository workplaceRepo;
        readonly IResumeRepository resumeRepo;

        public StatisticsController(IWorkplaceRepository workplaceRepo, IResumeRepository resumeRepo)
        {
            this.workplaceRepo = workplaceRepo;
            this.resumeRepo = resumeRepo;
        }

        [HttpGet]
        public Resume? GetYoungestPerson()
        {
            return resumeRepo.YoungestPerson();
        }

        [HttpGet]
        public Workplace? LongestWorkingTime()
        {
            return workplaceRepo.LongestWorkingTime();
        }
    }
}
