using Tugas2.Models;

namespace Tugas2.Services
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(int id);
        Task<Course> Insert(Course obj);
        Task<Course> Update(Course obj);
        Task Delete(int id);
    }
}
