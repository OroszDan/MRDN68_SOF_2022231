using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeController : ControllerBase
    {
        readonly IResumeRepository resumeRepo;
        readonly UserManager<IdentityUser> userManager;

        public ResumeController(IResumeRepository resumeRepo, UserManager<IdentityUser> userManager)
        {
            this.resumeRepo = resumeRepo;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<Resume> GetAll()
        {
            return resumeRepo.Read();
        }

        [HttpGet("{id}")]
        public Resume? GetOneById(string id)
        {
            return resumeRepo.ReadOneById(id);
        }

        [HttpPost]
        [Authorize]
        public async void Create([FromBody] Resume newResume)
        {
            newResume.Id = Guid.NewGuid().ToString();
            string userName = userManager.GetUserId(this.User);
            newResume.OwnerId = userManager.Users.FirstOrDefault(u => u.UserName == userName)?.Id;
            
            resumeRepo.Create(newResume);
        }

        [HttpPut]
        [Authorize]
        public void Update([FromBody] Resume resume)
        {
            resumeRepo.Update(resume);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(string id)
        {
            resumeRepo.DeleteById(id);
        }
    }
}
