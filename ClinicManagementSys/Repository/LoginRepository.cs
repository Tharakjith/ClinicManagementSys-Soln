using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ClinicManagementSysContext _context;
        public LoginRepository(ClinicManagementSysContext context)
        {
            _context = context;
        }

        public async Task<LoginRegistrationViewModel> ValidateUsers(string username, string password)
        {
            try
            {
                if (_context != null)
                {
                    // Join LoginRegistration with Doctor table to get DoctorId when role is doctor
                    var dbUser = await _context.LoginRegistrations
                        .Where(u => u.Username == username && u.Password == password)
                        .Select(u => new LoginRegistrationViewModel
                        {
                            Username = u.Username,
                            RoleId = u.RoleId,
                            RegistrationId = u.RegistrationId,
                            DoctorId = u.RoleId == 2 ? // 2 is Doctor RoleId
                                _context.Doctors
                                    .Where(d => d.RegistrationId == u.RegistrationId)
                                    .Select(d => d.DoctorId)
                                    .FirstOrDefault()
                                : null
                        })
                        .FirstOrDefaultAsync();

                    return dbUser;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
