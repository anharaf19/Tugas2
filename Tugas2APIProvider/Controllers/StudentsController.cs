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
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _studentServices;
        private readonly IMapper _mapper;

        public StudentsController(IStudent studentServices, IMapper mapper)
        {
            _studentServices = studentServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentViewModel>> Get()
        {
            var results = await _studentServices.GetAll();
            var student = _mapper.Map<IEnumerable<StudentViewModel>>(results);

            return student;
        }
        [HttpGet("{id}")]
        public async Task<StudentViewModel> Get(int id)
        {
            
            var result = await _studentServices.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var student = _mapper.Map<StudentViewModel>(result);

            return student;
        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentViewModel studentobj)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(studentobj);
                var result = await _studentServices.Insert(newStudent);
                var studentread = _mapper.Map<StudentViewModel>(result);

                return CreatedAtAction("Get", new { id = result.ID }, studentread);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(StudentViewModel studentobj)
        {
            try
            {
                var updateStudent = new Student
                {
                    ID = studentobj.ID, 
                    FirstMidName = studentobj.FirstMidName,
                    LastName = studentobj.LastName,
                    EnrollmentDate = studentobj.EnrollmentDate
                };
                var result = await _studentServices.Update(updateStudent);
                return Ok(studentobj);
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
                await _studentServices.Delete(id);
                return Ok($"Data Student dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<StudentViewModel>> GetByName(string name)
        {
            List<StudentViewModel> studentDtos = new List<StudentViewModel>();
            var results = await _studentServices.GetByName(name);
            foreach (var result in results)
            {
                studentDtos.Add(new StudentViewModel
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName,
                    EnrollmentDate = result.EnrollmentDate

                });
            }
            return studentDtos;
        }
        [HttpGet("getpaging")]
        public async Task<IEnumerable<StudentViewModel>> GetPaging(int pageNumber)
        {

            var results = await _studentServices.GetPaging(pageNumber);
            var students = _mapper.Map<IEnumerable<StudentViewModel>>(results);

            return students;
        }

    }
}
