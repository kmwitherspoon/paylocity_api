using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PaylocityAPI.Objects
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public double Salary { get; set; }
        public double TotalBenefitCost { get; set; }
        public double EmployeeBenefitCost { get; set; }
        public double DependentsBenefitCost { get; set; }
        public double Discount { get; set; }
        public List<Dependent> Dependents { get; set; }

        public Employee()
        {

        }

        public Employee(DataRow dr)
        {
            EmployeeId = Convert.IsDBNull(dr["EmployeeId"]) ? 0 : Convert.ToInt32(dr["EmpolyeeId"]);
            LastName = Convert.IsDBNull(dr["LastName"]) ? "" : Convert.ToString(dr["LastName"]);
            FirstName = Convert.IsDBNull(dr["FirstName"]) ? "" : Convert.ToString(dr["FirstName"]);
            Salary = Convert.IsDBNull(dr["Salary"]) ? 0 : Convert.ToDouble(dr["Salary"]);
        }

        public Employee(IDataReader dr)
        {
            EmployeeId = Convert.IsDBNull(dr["EmployeeId"]) ? 0 : Convert.ToInt32(dr["EmpolyeeId"]);
            LastName = Convert.IsDBNull(dr["LastName"]) ? "" : Convert.ToString(dr["LastName"]);
            FirstName = Convert.IsDBNull(dr["FirstName"]) ? "" : Convert.ToString(dr["FirstName"]);
            Salary = Convert.IsDBNull(dr["Salary"]) ? 0 : Convert.ToDouble(dr["Salary"]);
        }
    }
}
