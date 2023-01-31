using MRDN68_SOF_2022231.Models;
using System.Security.Claims;

namespace MRDN68_SOF_2022231.Data
{
    public interface IResumeRepository
    {
        void Create(Resume resume);
        void Delete(string id, ClaimsPrincipal principal);
        IEnumerable<Resume> Read();
        IEnumerable<Resume> ReadFromOwnerId(ClaimsPrincipal principal);
        Resume ReadOneById(string id);
        void Update(Resume resume);
        Resume? YoungestPerson();
    }
}