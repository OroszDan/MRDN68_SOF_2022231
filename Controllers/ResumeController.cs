using Microsoft.AspNetCore.Authorization;
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

        public ResumeController(IResumeRepository resumeRepo)
        {
            this.resumeRepo = resumeRepo;
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
        public void Create([FromBody] Resume newResume)
        {
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
