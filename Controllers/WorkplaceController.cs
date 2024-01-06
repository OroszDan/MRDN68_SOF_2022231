using Microsoft.AspNetCore.Mvc;
using MRDN68_SOF_2022231.Data;
using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkplaceController : ControllerBase
    {
        IWorkplaceRepository workplaceRepo;

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
        public Workplace? GetOneById(string id)
        {
            return workplaceRepo.ReadOneById(id);
        }

        [HttpPost]
        public void Create([FromBody] Workplace newWorkplace)
        {
            workplaceRepo.Create(newWorkplace);
        }

        [HttpPut]
        public void Update([FromBody] Workplace workplace)
        {
            workplaceRepo.Update(workplace);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            workplaceRepo.Delete(id);
        }
    }
}
