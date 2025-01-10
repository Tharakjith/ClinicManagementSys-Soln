using ClinicManagementSys.Model;
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


        public async Task<LoginRegistration> ValidateUsers(string username, string password)
        {
            try
            {
                if (_context != null)
                {
                    LoginRegistration? dbUser = await _context.LoginRegistrations.FirstOrDefaultAsync(
                        u => u.Username == username && u.Password == password);

                    if (dbUser != null)
                    {
                        return dbUser;
                    }
                }
                //Return an empty if _context is null
                return null;
            }
            catch (Exception ex)
            {
                //return StatusCode(500, $"Internal server error : {ex.Message}"); 
                return null;
            }

        }
    }
}
