using Tugas2APIProvider.ViewModels;

namespace Tugas2APIProvider.Services
{
    public interface IUser
    {
        Task Registration(CreateUserViewModel user);
        Task<UserViewModel> Authenticate(string username,string password);
        Task<IEnumerable<UserViewModel>> GetAll();
    }
}
