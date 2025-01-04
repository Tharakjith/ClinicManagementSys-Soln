using ClinicManagementSys.Model;

namespace ClinicManagementSys.Repository
{
    public interface ILoginRepository
    {
        public Task<LoginRegistration> ValidateUsers(string username, string password);
    }
}
