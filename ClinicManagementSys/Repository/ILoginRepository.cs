using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;

namespace ClinicManagementSys.Repository
{
    public interface ILoginRepository
    {
        public Task<LoginRegistrationViewModel> ValidateUsers(string username, string password);
    }
}
