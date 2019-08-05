using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Http;
using System.Web;
using PaylocityAPI.CORE;
using System.Configuration;

namespace PaylocityAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var employeeCore = new EmployeeCore(connectionString);
            return Ok(employeeCore.GetAllEmployees());
        }

        [HttpGet]
        [Route("GetAllDependents")]
        public IHttpActionResult GetAllDependents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var employeeCore = new EmployeeCore(connectionString);
            return Ok(employeeCore.GetAllDependents());
        }

        [HttpGet]
        [Route("GetAllEmployeeInformation")]
        public IHttpActionResult GetAllEmployeeInformation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var employeeCore = new EmployeeCore(connectionString);
            return Ok(employeeCore.GetAllEmployeeInformation());
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public IHttpActionResult InsertEmployee(string lastName, string firstName, double salary)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var employeeCore = new EmployeeCore(connectionString);
            return Ok(employeeCore.InsertEmployee(lastName, firstName, salary));
        }

        [HttpPost]
        [Route("InsertDependent")]
        public IHttpActionResult InsertDependent(int employeeId, string lastName, string firstName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var employeeCore = new EmployeeCore(connectionString);
            return Ok(employeeCore.InsertDependent(employeeId, lastName, firstName));
        }
    }

}
