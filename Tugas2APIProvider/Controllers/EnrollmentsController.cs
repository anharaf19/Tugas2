using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2APIProvider.Models;
using Tugas2APIProvider.Services;
using Tugas2APIProvider.ViewModels;

namespace Tugas2APIProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollmentServices;
        private readonly IMapper _mapper;

        public EnrollmentsController(IEnrollment enrollmentServices, IMapper mapper)
        {
            _enrollmentServices = enrollmentServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EnrollmentViewModel>> Get()
        {
            var results = await _enrollmentServices.GetAll();
            var enrollment = _mapper.Map<IEnumerable<EnrollmentViewModel>>(results);

            return enrollment;
        }
        [HttpGet("{id}")]
        public async Task<EnrollmentViewModel> Get(int id)
        {
            
            var result = await _enrollmentServices.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var enrollment = _mapper.Map<EnrollmentViewModel>(result);

            return enrollment;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EnrollmentViewModel enrollmentobj)
        {
            try
            {
                var newEnrollment = _mapper.Map<Enrollment>(enrollmentobj);
                var result = await _enrollmentServices.Insert(newEnrollment);
                var enrollmentread = _mapper.Map<EnrollmentViewModel>(result);

                return CreatedAtAction("Get", new { id = result.EnrollmentID }, enrollmentread);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(EnrollmentViewModel enrollmentobj)
        {
            try
            {
                var updateEnrollment = new Enrollment
                {
                    EnrollmentID = enrollmentobj.EnrollmentID, 
                    CourseID = enrollmentobj.CourseID,
                    StudentID = enrollmentobj.StudentID,
                    
                };
                var result = await _enrollmentServices.Update(updateEnrollment);
                return Ok(enrollmentobj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _enrollmentServices.Delete(id);
                return Ok($"Data Enrollment dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
