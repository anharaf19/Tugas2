using Tugas2APIProvider.Models;

namespace Tugas2APIProvider.Services
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string name);
        Task<IEnumerable<Student>> GetPaging(int pageNumber);
    }
}
