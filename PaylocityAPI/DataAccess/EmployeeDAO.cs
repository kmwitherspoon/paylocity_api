using System;
using System.Data;
using Generic.Repository.EntityFramework;
using System.Data.SqlClient;
using System.Collections.Generic;
using PaylocityAPI.Objects;

namespace PaylocityAPI.DataAccess
{
    public class EmployeeDAO
    {
        public string ConnectionString { get; set; }

        public EmployeeDAO(string connectionString) => ConnectionString = connectionString;


        public List<Employee> GetListofEmployees()
        {
            string procedureName = "[dbo].[GetAllEmployees]";
            var result = new List<Employee>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = int.Parse(reader[0].ToString());
                            string lastName = reader[1].ToString();
                            string firstName = reader[2].ToString();
                            double salary = double.Parse(reader[3].ToString());
                            Employee tmpRecord = new Employee()
                            {
                                EmployeeId = id,
                                LastName = lastName,
                                FirstName = firstName,
                                Salary = salary
                            };
                            result.Add(tmpRecord);
                        }
                    }
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Error occured while getting Data from GetAllEmployees", new Exception(ex.ToString()));
            }

            return result;
        }

        public List<Dependent> GetListofDependents()
        {
            string procedureName = "[dbo].[GetAllDependents]";
            var result = new List<Dependent>();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = int.Parse(reader[0].ToString());
                            int dId = int.Parse(reader[1].ToString());
                            string lastName = reader[2].ToString();
                            string firstName = reader[3].ToString();
                            Dependent tmpRecord = new Dependent()
                            {
                                EmployeeId = id,
                                DependentId = dId,
                                LastName = lastName,
                                FirstName = firstName,
                            };
                            result.Add(tmpRecord);
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while getting Data from GetAllDependents", new Exception(ex.ToString()));
            }

            return result;
        }

        public int InsertEmployee(string lastName, string firstName, double salary)
        {
            int employeeId = 0;

            string procedureName = "[dbo].[InsertEmployee]";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@LastName", lastName));
                    command.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    command.Parameters.Add(new SqlParameter("@Salary", salary));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeId = int.Parse(reader[0].ToString());
                        }

                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while inserting Data to " + procedureName + " with params," + lastName + ", " + firstName, new Exception(ex.ToString()));
            }
            return employeeId;
        }

        public int InsertDependent(string storedProc, int employeeId, string lastName, string firstName)
        {
            int dependentId = 0;
            string procedureName = "[dbo].[InsertDependent]";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                    command.Parameters.Add(new SqlParameter("@LastName", lastName));
                    command.Parameters.Add(new SqlParameter("@FirstName", firstName));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dependentId = int.Parse(reader[0].ToString());
                        }

                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while inserting Data to " + procedureName + " with params," + lastName + ", " + firstName + ", " + employeeId, new Exception(ex.ToString()));
            }

            return dependentId;
        }
    }
}
