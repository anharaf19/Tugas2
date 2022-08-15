using AutoMapper;
using Tugas2APIProvider.Models;
using Tugas2APIProvider.ViewModels;

namespace Tugas2APIProvider.Profiles
{
    public class StudentProfiles : Profile
    {
        public StudentProfiles()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();
        }

    }
}
