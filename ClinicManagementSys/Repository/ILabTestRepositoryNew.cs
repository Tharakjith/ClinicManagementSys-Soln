using ClinicManagementSys.Model;
using ClinicManagementSys.ViewModel;

namespace ClinicManagementSys.Repository
{
    public interface ILabTestRepositoryNew
    {
        Task<List<AppPatStaLabViewModel>> GetTodaysPrescribedLabTests();
        Task<Labtest> GetLabTestDetails(int labTestId);
        Task<LabTestReport> CreateLabTestReport(LabTestReport report);
        public Task<LabTestReportViewModel> GetReportDetailsByReportId(int reportId);
        Task<LabtestBillViewModel> GetBillDetails(int reportId);

    }
}