using System;
using System.Data;
namespace PaylocityAPI.Objects

{
    public class Dependent
    {
        public int EmployeeId { get; set; }
        public int DependentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public double BenefitCost { get; set; }
        public double Discount { get; set; }

        public Dependent()
        {

        }

        public Dependent(DataRow dr)
        {
            EmployeeId = Convert.IsDBNull(dr["EmployeeId"]) ? 0 : Convert.ToInt32(dr["EmpolyeeId"]);
            DependentId = Convert.IsDBNull(dr["DependentId"]) ? 0 : Convert.ToInt32(dr["DependentId"]);
            LastName = Convert.IsDBNull(dr["LastName"]) ? "" : Convert.ToString(dr["LastName"]);
            FirstName = Convert.IsDBNull(dr["FirstName"]) ? "" : Convert.ToString(dr["FirstName"]);
        }

        public Dependent(IDataReader dr)
        {
            EmployeeId = Convert.IsDBNull(dr["EmployeeId"]) ? 0 : Convert.ToInt32(dr["EmpolyeeId"]);
            DependentId = Convert.IsDBNull(dr["DependentId"]) ? 0 : Convert.ToInt32(dr["DependentId"]);
            LastName = Convert.IsDBNull(dr["LastName"]) ? "" : Convert.ToString(dr["LastName"]);
            FirstName = Convert.IsDBNull(dr["FirstName"]) ? "" : Convert.ToString(dr["FirstName"]);
        }
    }
}
