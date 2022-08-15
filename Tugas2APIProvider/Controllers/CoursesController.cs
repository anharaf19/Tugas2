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
    public class CoursesController : ControllerBase
    {
        private readonly ICourse _CourseServices;
        private readonly IMapper _mapper;

        public CoursesController(ICourse courseServices, IMapper mapper)
        {
            _CourseServices = courseServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get()
        {
            var results = await _CourseServices.GetAll();
            var course = _mapper.Map<IEnumerable<CourseViewModel>>(results);

            return course;
        }

        [HttpGet("{id}")]
        public async Task<CourseViewModel> Get(int id)
        {

            var result = await _CourseServices.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var course = _mapper.Map<CourseViewModel>(result);

            return course;
        }
        [HttpPost]
        public async Task<ActionResult> Post(CourseViewModel courseobj)
        {
            try
            {
                var newCourse = _mapper.Map<Course>(courseobj);
                var result = await _CourseServices.Insert(newCourse);
                var courseread = _mapper.Map<CourseViewModel>(result);

                return CreatedAtAction("Get", new { id = result.CourseID }, courseread);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(CourseViewModel courseobj)
        {
            try
            {
                var updateCourse = new Course
                {
                    CourseID =courseobj.CourseID,
                    Title = courseobj.Title,
                    Credits = courseobj.Credits
                };
                var result = await _CourseServices.Update(updateCourse);
                return Ok(courseobj);
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
                await _CourseServices.Delete(id);
                return Ok($"Data Course dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByName")]
        public async Task<IEnumerable<CourseViewModel>> GetByName(string name)
        {
            List<CourseViewModel> courseDtos = new List<CourseViewModel>();
            var results = await _CourseServices.GetByName(name);
            foreach (var result in results)
            {
                courseDtos.Add(new CourseViewModel
                {
                    CourseID = result.CourseID,
                    Title = result.Title,
                    Credits = result.Credits

                });
            }
            return courseDtos;
        }
        [HttpGet("getpaging")]
        public async Task<IEnumerable<CourseViewModel>> GetPaging(int pageNumber)
        {

            var results = await _CourseServices.GetPaging(pageNumber);
            var courses = _mapper.Map<IEnumerable<CourseViewModel>>(results);

            return courses;
        }

    }
}
