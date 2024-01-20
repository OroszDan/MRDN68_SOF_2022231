using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkplaceController : ControllerBase
    {
        readonly IWorkplaceRepository workplaceRepo;

        public WorkplaceController(IWorkplaceRepository workplaceRepo)
        {
            this.workplaceRepo = workplaceRepo;
        }

        [HttpGet]
        public IEnumerable<Workplace> GetAll()
        {
            return workplaceRepo.Read();
        }

        [HttpGet("{id}")]
        public Workplace? ReadOneById(string id)
        {
            return workplaceRepo.ReadOneById(id);
        }

        [HttpPost]
        [Authorize]
        public void Create([FromBody] Workplace newWorkplace)
        {
            newWorkplace.Id = Guid.NewGuid().ToString();
            workplaceRepo.Create(newWorkplace);
        }

        [HttpPut]
        [Authorize]
        public void Update([FromBody] Workplace workplace)
        {
            workplaceRepo.Update(workplace);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(string id)
        {
            workplaceRepo.Delete(id);
        }
    }
}
