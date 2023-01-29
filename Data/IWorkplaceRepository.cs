using MRDN68_SOF_2022231.Models;

namespace MRDN68_SOF_2022231.Data
{
    public interface IWorkplaceRepository
    {
        void Create(Workplace workplace);
        IEnumerable<Workplace> Read();
        Workplace? ReadFromId(string id);
    }
}