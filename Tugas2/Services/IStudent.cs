using Tugas2.Models;

namespace Tugas2.Services
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<Student> Insert(Student obj);
        Task<Student> Update(Student obj);
        Task Delete(int id);
    }
}
