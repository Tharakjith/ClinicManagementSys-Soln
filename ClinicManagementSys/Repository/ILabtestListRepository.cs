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

        ////3- Get All Employees using ViewModel;
        //public Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployees();

        ////4 - Update an Employee with ID and employee
        //public Task<ActionResult<TblEmployee>> PutTblEmployee(int id, TblEmployee tblEmployee);

        ////5 - Insert an employee - RetuRN Employee Record
        //public Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee tblEmployee);
    }
}
