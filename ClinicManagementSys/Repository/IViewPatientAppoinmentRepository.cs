using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface IViewPatientAppoinmentRepository
    {
      Task<IEnumerable<StartDiagnosysViewmodel>> GetTodaysAppointmentsAsync(int doctorId);
    }
}
