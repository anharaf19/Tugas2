using AutoMapper;
using Tugas2APIProvider.Models;
using Tugas2APIProvider.ViewModels;

namespace Tugas2APIProvider.Profiles
{
    public class CourseProfiles : Profile
    {
        public CourseProfiles()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();
        }
    }
}
