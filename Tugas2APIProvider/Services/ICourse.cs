using Tugas2APIProvider.Models;

namespace Tugas2APIProvider.Services
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByName(string name);
        Task<IEnumerable<Course>> GetPaging(int pageNumber);
    }
}
