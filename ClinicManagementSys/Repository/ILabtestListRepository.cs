using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSys.Repository
{
    public interface ILabtestListRepository
    {
        //1- Get All Employees using ViewModel;
        public Task<ActionResult<IEnumerable<AppPatStaLabViewModel>>> GetViewModelLabtestList();

        //2 -Get an Employee based on Id
        public Task<AppPatStaLabViewModel> GetLabtestById(int tokenNumber);

        //3- Get All Employees using ViewModel;
        public Task<ActionResult<IEnumerable<LabtestBillViewModel>>> GetViewModelLabtestBill();

        public Task<IEnumerable<LabTestReportViewModel>> GetAllLabTestReportsAsync();
        public Task<LabTestReportViewModel> GetLabTestReportByIdAsync(int id);
        public Task<bool> UpdateLabTestReportAsync(int id, LabTestReportViewModel model);
    }
}
