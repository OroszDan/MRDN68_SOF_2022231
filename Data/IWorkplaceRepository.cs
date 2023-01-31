using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Data
{
    public interface IWorkplaceRepository
    {
        void Create(Workplace workplace);
        void Delete(string id);
        Workplace? LongestWorkingTime();
        IEnumerable<Workplace> Read();
        IEnumerable<Workplace> ReadFromUid(string uid);
        Workplace? ReadOneById(string id);
        void Update(Workplace workplace);
    }
}