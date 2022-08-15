using Microsoft.EntityFrameworkCore;
using Tugas2APIProvider.Models;

namespace Tugas2APIProvider.Services
{
    public class EnrollmentServices : IEnrollment
    {
        private readonly AppDbContext _context;

        public EnrollmentServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteEnrollment = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
                if (deleteEnrollment == null)
                    throw new Exception($"Data Course dengan id {id} tidak ditemukan");
                _context.Enrollments.Remove(deleteEnrollment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.OrderBy(s => s.EnrollmentID).ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var result = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
            if (result == null) throw new Exception($"Data Enrollment dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _context.Enrollments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            try
            {
                var updateEnrollment = await _context.Enrollments.FirstOrDefaultAsync(s => s.CourseID == obj.CourseID);
                if (updateEnrollment == null)
                    throw new Exception($"Data Student dengan id {obj.CourseID} tidak ditemukan");

                updateEnrollment.CourseID = obj.CourseID;
                updateEnrollment.StudentID = obj.StudentID;
                updateEnrollment.Grade = obj.Grade;
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
