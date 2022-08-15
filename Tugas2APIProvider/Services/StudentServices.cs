using Microsoft.EntityFrameworkCore;
using Tugas2APIProvider.Models;

namespace Tugas2APIProvider.Services
{
    public class StudentServices : IStudent
    {
        private readonly AppDbContext _context;

        public StudentServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteStudent = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                if (deleteStudent == null)
                    throw new Exception($"Data Student dengan id {id} tidak ditemukan");
                _context.Students.Remove(deleteStudent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.FirstMidName).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Data Student dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Student>> GetByName(string name)
        {
            var students = await _context.Students.Where(s => s.FirstMidName.Contains(name))
              .OrderBy(s => s.FirstMidName).ToListAsync();
            return students;
        }

        public async Task<IEnumerable<Student>> GetPaging(int pageNumber)
        {
            var s = await _context.Students.Include(s => s.FirstMidName)
               .OrderBy(s => s.FirstMidName).AsNoTracking().ToListAsync();


            int numberOfObjectsPerPage = 10;
            var queryResultPage = s
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return queryResultPage;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Student> Update(Student obj)
        {
            try
            {
                var updateStudent = await _context.Students.FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (updateStudent == null)
                    throw new Exception($"Data Student dengan id {obj.ID} tidak ditemukan");

               
                updateStudent.FirstMidName = obj.FirstMidName;
                updateStudent.LastName = obj.LastName;
                updateStudent.EnrollmentDate = obj.EnrollmentDate;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
