using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabtestListsController : ControllerBase
    {
        //Call repository
        private readonly ILabtestListRepository _repository;

        //DI Constructor Injection
        public LabtestListsController(ILabtestListRepository repository)
        {
            _repository = repository;
        }


        #region 1- get all labtests-search all
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<AppPatStaLabViewModel>>> GetAllLabtestByViewModel()
        {
            var employees = await _repository.GetViewModelLabtestList();
            if (employees == null)
            {
                return NotFound("No Labtests found");
            }

            return Ok(employees);
        }

        #endregion

        #region 2- get all labtests-search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppPatStaLabViewModel>> GetLabtestById(int tokenNumber)
        {
            var employees = await _repository.GetLabtestById(tokenNumber);
            if (employees == null)
            {
                return NotFound("No Employees found");
            }

            return Ok(employees);
        }

        #endregion

        //#region 2- get all employees-search all
        //[HttpGet("vm")]
        //public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetAllEmployeesByViewModel()
        //{
        //    var employees = await _repository.GetViewModelEmployees();
        //    if (employees == null)
        //    {
        //        return NotFound("No Employees found");
        //    }

        //    return Ok(employees);
        //}

        //#endregion

        //#region 6 - Update Employee - Return  EmployeeRecord
        //[HttpPut("{id}")]

        //public async Task<ActionResult<TblEmployee>> UpdatePutTblEmployee(int id, TblEmployee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //insert a new record and return as an object named employee
        //        var updateEmployee = await _repository.PutTblEmployee(id, employee);
        //        if (updateEmployee != null)
        //        {
        //            return Ok(updateEmployee);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();

        //}

        //#endregion

        //#region 4 -  Insert an Employee - Return Employee Record
        //[HttpPost]
        //public async Task<ActionResult<TblEmployee>> InsertPostTblEmployeesReturnRecord(TblEmployee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //insert a new record and return as an object named employee
        //        var newEmployee = await _repository.PostTblEmployeesReturnRecord(employee);
        //        if (newEmployee != null)
        //        {
        //            return Ok(newEmployee);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}
        //#endregion


        ////// GET: api/<LabtestListsController>
        ////[HttpGet]
        ////public IEnumerable<string> Get()
        ////{
        ////    return new string[] { "value1", "value2" };
        ////}

        ////// GET api/<LabtestListsController>/5
        ////[HttpGet("{id}")]
        ////public string Get(int id)
        ////{
        ////    return "value";
        ////}

        ////// POST api/<LabtestListsController>
        ////[HttpPost]
        ////public void Post([FromBody] string value)
        ////{
        ////}

        ////// PUT api/<LabtestListsController>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        ////// DELETE api/<LabtestListsController>/5
        ////[HttpDelete("{id}")]
        ////public void Delete(int id)
        ////{
        ////}
    }
}
