using Microsoft.EntityFrameworkCore;
using Tugas2APIProvider.Models;

namespace Tugas2APIProvider.Services
{
    public class CourseServices : ICourse
    {
        private readonly AppDbContext _context;

        public CourseServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteCourse = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
                if (deleteCourse == null)
                    throw new Exception($"Data Course dengan id {id} tidak ditemukan");
                _context.Courses.Remove(deleteCourse);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(s => s.Title).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
            if (result == null) throw new Exception($"Data Course dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Course>> GetByName(string name)
        {
            var courses = await _context.Courses.Where(s => s.Title.Contains(name))
              .OrderBy(s => s.Title).ToListAsync();
            return courses;
        }

        public async Task<IEnumerable<Course>> GetPaging(int pageNumber)
        {
            var s = await _context.Courses.Include(s => s.Title)
               .OrderBy(s => s.Title).AsNoTracking().ToListAsync();


            int numberOfObjectsPerPage = 10;
            var queryResultPage = s
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return queryResultPage;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Course> Update(Course obj)
        {
            try
            {
                var updateCourse = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == obj.CourseID);
                if (updateCourse == null)
                    throw new Exception($"Data Student dengan id {obj.CourseID} tidak ditemukan");

                updateCourse.Title = obj.Title;
                updateCourse.Credits = obj.Credits;
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
