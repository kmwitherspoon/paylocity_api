using System;
using System.Collections.Generic;
using PaylocityAPI.Objects;
using PaylocityAPI.DataAccess;

namespace PaylocityAPI.CORE
{
    public class EmployeeCore
    {

        public string ConnectionString { get; set; }

        public EmployeeCore(string connectionString) => ConnectionString = connectionString;


        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            var employeeDAO = new EmployeeDAO(ConnectionString);

            try
            {
                employees = employeeDAO.GetListofEmployees();

            }
            catch (Exception ex){
                throw new Exception("Error occured when trying to GetAllEmployees", new Exception(ex.ToString()));
            }
            return employees;
        }

        public List<Dependent> GetAllDependents()
        {
            List<Dependent> dependents = new List<Dependent>();
            List<Dependent> updatedDependents = new List<Dependent>();
            Dependent dependent = new Dependent();
            var employeeDAO = new EmployeeDAO(ConnectionString);
            double dependentCost = 500;
            double discountPercentage = 0.1;

            try
            {
                dependents = employeeDAO.GetListofDependents();
                foreach(Dependent d in dependents)
                {
                    d.BenefitCost = dependentCost;

                    //checking if first name starts with a
                    //if so discount 10% of $500
                    if (d.FirstName.StartsWith("A", StringComparison.Ordinal))
                    {
                        double discount = dependentCost * discountPercentage;
                        d.BenefitCost -= discount;
                        d.Discount = discount;
                    }
                    updatedDependents.Add(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured when trying to GetAllDependents", new Exception(ex.ToString()));
            }
            return updatedDependents;
        }

        public int InsertEmployee(string lastName, string firstName, double salary)
        {
            int employeeId = 0;
            var employeeDAO = new EmployeeDAO(ConnectionString);

            try
            {
                employeeId = employeeDAO.InsertEmployee(lastName, firstName, salary);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured when trying to InsertEmployee", new Exception(ex.ToString()));
            }
            return employeeId;
        }

        public int InsertDependent(int employeeId, string lastName, string firstName)
        {
            int dependentId = 0;
            var employeeDAO = new EmployeeDAO(ConnectionString);
            string storedProc = "dbo.InsertDependent";
            try
            {
                dependentId = employeeDAO.InsertDependent(storedProc, employeeId, lastName, firstName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured when trying to InsertDependent", new Exception(ex.ToString()));
            }
            return dependentId;
        }

        public List<Employee> GetAllEmployeeInformation()
        {
            List<Employee> employees = GetAllEmployees();
            List<Dependent> dependents = GetAllDependents();

            List<Employee> mappedEmployees = MapDependentsToEmployee(employees, dependents);

            List<Employee> updatedEmployeeLList = CalculateBenefits(mappedEmployees);

            return updatedEmployeeLList;

        }

        public List<Employee> MapDependentsToEmployee(List<Employee> employees, List<Dependent> dependents)
        {
            foreach (Employee e in employees)
            {
                e.Dependents = new List<Dependent>();
                foreach (Dependent d in dependents)
                {
                    if (d.EmployeeId == e.EmployeeId)
                    {                        
                        e.Dependents.Add(d);
                    }
                }


            }

            return employees;
        }

        public List<Employee> CalculateBenefits(List<Employee> employees)
        {
            double employeeCost = 1000;
            double dependentCost = 500;
            double discountPercentage = 0.1;

            foreach(Employee e in employees)
            {
                double totalDependentCost = 0;
                double employeeDiscount = 0;
                e.EmployeeBenefitCost = employeeCost;               

                //checking if first name starts with a
                //if so discount 10% from benefit cost
                if (e.FirstName.StartsWith("A", StringComparison.Ordinal))
                {                   
                    employeeDiscount = e.EmployeeBenefitCost * discountPercentage;
                    e.EmployeeBenefitCost -= employeeDiscount;
                }

                if (e.Dependents.Count > 0)
                {
                    int dependentCount = e.Dependents.Count;
                    double discount = 0;

                    foreach (Dependent d in e.Dependents)
                    {
                        double dependentDiscount = 0;
                        d.BenefitCost = dependentCost;

                        //checking if first name starts with a
                        //if so discount 10% of $500
                        if (d.FirstName.StartsWith("A", StringComparison.Ordinal))
                        {
                            dependentDiscount += dependentCost * discountPercentage;
                            d.BenefitCost -= discount;
                            d.Discount = dependentDiscount;
                            discount += dependentDiscount;
                        }
                        totalDependentCost += d.BenefitCost;
                    }
                    //final benefit cost
                    e.TotalBenefitCost = (e.EmployeeBenefitCost + (dependentCount * dependentCost)) - discount;
                    //adding up total discount between employee and dependents
                    e.Discount = discount + employeeDiscount;
                    //getting total dependent cost
                    e.DependentsBenefitCost = totalDependentCost;
                }
                else
                {
                    e.TotalBenefitCost = e.EmployeeBenefitCost;
                }

            }

            return employees;
        }

        
    }
}
