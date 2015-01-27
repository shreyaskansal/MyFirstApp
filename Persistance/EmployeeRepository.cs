using Core.Models;
using log4net;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class EmployeeRepository
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private System.Data.SqlClient.SqlConnection conn;

        /// <summary>
        /// Establishes Connection
        /// </summary>
        public EmployeeRepository()
        {
            conn = ConnectionManager.GetConnection();
            conn.Open();
        }

        /// <summary>
        /// Get All the Employee List
        /// List<EmployeeModel> GetEmployeeList()"
        /// </summary>
        /// <returns></returns>        
        public List<EmployeeModel> GetEmployeeList()
        {
            SqlCommand sqlcmd = null;
            List<EmployeeModel> ListOfEmployees = null;

            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.PlainTextCommand(conn, string.Format("SELECT ID, NAME, DOB, Address FROM EMPLOYEE"));
                    // Add Parameters

                    connection.Open();
                    ListOfEmployees = new List<EmployeeModel>();
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeModel employee = new EmployeeModel();
                            employee.ID = Convert.ToInt16(reader["ID"]);
                            employee.Name = Convert.ToString(reader["Name"]);
                            employee.DOB = Convert.ToDateTime(reader["DOB"]);
                            employee.Username = Convert.ToString(reader["Address"]);
                            ListOfEmployees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception("An Error Occured");
            }
            return ListOfEmployees;
        }

        /// <summary>
        /// Get the Employee List using Pagination
        /// Search Employee Name in the database
        /// EmployeeList JsonUser(int Offset, int PageSize, string SearchEmployeeName)
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="PageSize"></param>
        /// <param name="SearchEmployeeName"></param>
        /// <returns></returns>
        public ResultList<EmployeeModel> JsonUser(int Offset, int PageSize, string SearchEmployeeName)
        {
            SqlCommand sqlcmd = null;
            ResultList<EmployeeModel> getlist = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_DisplayEmployees");
                    sqlcmd.Parameters.Add("Offset", System.Data.SqlDbType.Int).Value = Offset;
                    sqlcmd.Parameters.Add("PageSize", System.Data.SqlDbType.Int).Value = PageSize;
                    sqlcmd.Parameters.Add("SearchEmployeeName", System.Data.SqlDbType.VarChar).Value = SearchEmployeeName;
                    connection.Open();

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        getlist = new ResultList<EmployeeModel>();
                        getlist.list = new List<EmployeeModel>();

                        while (reader.Read())
                        {
                            EmployeeModel employee = new EmployeeModel();
                            employee = new EmployeeModel();
                            employee.ID = Convert.ToInt16(reader["ID"]);
                            employee.Name = Convert.ToString(reader["Name"]);
                            employee.DOB = Convert.ToDateTime(reader["DOB"]);
                            employee.Username = Convert.ToString(reader["Address"]);
                            getlist.list.Add(employee);
                        }

                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                getlist.TotalCount = Convert.ToInt16(reader["TotalCount"]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception("An Error Occured");
            }
            return getlist;
        }

        /// <summary>
        /// Get the Details of a particular Employee
        /// EmployeeModel EmployeeDetails(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public EmployeeModel EmployeeDetails(int EmployeeId)
        {
            SqlCommand sqlcmd = null;
            EmployeeModel employee = new EmployeeModel();
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_DisplayDetailsOfEmployee");
                    sqlcmd.Parameters.Add("EmployeeId", System.Data.SqlDbType.Int).Value = EmployeeId;
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = new EmployeeModel();
                            employee.ID = Convert.ToInt16(reader["ID"]);
                            employee.Name = Convert.ToString(reader["Name"]);
                            employee.DOB = Convert.ToDateTime(reader["DOB"]);
                            employee.Username = Convert.ToString(reader["Email"]);
                            employee.Address = Convert.ToString(reader["Address"]);
                            employee.Department = Convert.ToString(reader["Dept_Name"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception("An Error Occured");
            }
            return employee;
        }

        /// <summary>
        /// Get the Details of a particular Employee
        /// DepartmentModel Department()
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public ResultList<Core.Models.DepartmentModel> Department()
        {
            SqlCommand sqlcmd = null;
            DepartmentModel department = null;
          
            ResultList<Core.Models.DepartmentModel> model = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.PlainTextCommand(conn, "SELECT ID, DEPT_NAME FROM DEPT;");
                    model = new ResultList<DepartmentModel>();
                    model.list = new List<DepartmentModel>();
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            department = new DepartmentModel();
                            department.Id= Convert.ToInt16(reader["ID"]);
                            department.Name = Convert.ToString(reader["Dept_Name"]);
                            model.list.Add(department);
                        }
                                             
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception(e.Message);
            }
            return model;
        }

        /// <summary>
        /// Get the Employee List using Pagination
        /// Search Employee Name in the database
        /// EmployeeList JsonUser(int Offset, int PageSize, string SearchEmployeeName)
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="PageSize"></param>
        /// <param name="SearchEmployeeName"></param>
        /// <returns></returns>
        public ResultList<DepartmentModel> JsonDepartment(int Offset, int PageSize, string SearchDepartmentName)
        {
            SqlCommand sqlcmd = null;
            ResultList<DepartmentModel> getlist = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_DepartmentList");
                    sqlcmd.Parameters.Add("Offset", System.Data.SqlDbType.Int).Value = Offset;
                    sqlcmd.Parameters.Add("PageSize", System.Data.SqlDbType.Int).Value = PageSize;
                    sqlcmd.Parameters.Add("SearchDepartmentName", System.Data.SqlDbType.VarChar).Value = SearchDepartmentName;
                    connection.Open();

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        getlist = new ResultList<DepartmentModel>();
                        getlist.list = new List<DepartmentModel>();
                        DepartmentModel dept = null;
                        while (reader.Read())
                        {
                            DepartmentModel department = new DepartmentModel();
                            department.elist = new List<EmployeeModel>();
                            department.Id = Convert.ToInt16(reader["ID"]);
                            department.Name = Convert.ToString(reader["NAME"]);
                            department.EmployeeCount = Convert.ToInt16(reader["EMPLOYEE_COUNT"]);
                            getlist.list.Add(department);
                        }

                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                getlist.TotalCount = Convert.ToInt16(reader["Total"]);
                            }
                        }

                        

                        if (reader.NextResult())
                        {
                            
                            while(reader.Read())
                            {
                                EmployeeModel employee = new EmployeeModel();
                                employee.DepartmentId = Convert.ToUInt16(reader["DeptId"]);
                                employee.Name = Convert.ToString(reader["Name"]);

                                dept = getlist.list.Where(d => d.Id == employee.DepartmentId).FirstOrDefault();
                                dept.elist.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception(e.Message);
            }
            return getlist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public ResultList<DepartmentModel> DepartmentDetails(int DepartmentId)
        {
            SqlCommand sqlcmd = null;
            DepartmentModel department = null;
            ResultList<DepartmentModel> getlist = null;
                    
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_DisplayEmployeesOfDepartment");
                    sqlcmd.Parameters.Add("DepartmentId", System.Data.SqlDbType.Int).Value = DepartmentId;
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        getlist = new ResultList<DepartmentModel>();
                        getlist.list = new List<DepartmentModel>();
                 
                        while (reader.Read())
                        {
                            department = new DepartmentModel();
                            department.Name = Convert.ToString(reader["Name"]);
                            getlist.list.Add(department);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception(e.Message);
            }
            return getlist;
        }
        /// <summary>
        /// Function to add an Employee in the Database
        /// void AddEmployeemodel(AddEmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        public void AddEmployeemodel(AddEmployeeModel model)
        {
            SqlCommand sqlcmd = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_AddEmployee");
                    sqlcmd.Parameters.Add("Name", System.Data.SqlDbType.VarChar).Value = model.Name;
                    sqlcmd.Parameters.Add("DOB", System.Data.SqlDbType.VarChar).Value = model.DOB;
                    sqlcmd.Parameters.Add("Address", System.Data.SqlDbType.VarChar).Value = model.Address;
                    sqlcmd.Parameters.Add("Email", System.Data.SqlDbType.VarChar).Value = model.Username;
                    sqlcmd.Parameters.Add("DeptId", System.Data.SqlDbType.Int).Value = model.Department;
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e.Message);
                throw new Exception("An Error Occured");
            }
        }


        /// <summary>
        /// Function that returns the model of Dept Table with DeptId and DeptName
        /// List<Department> GetDepartment()
        /// </summary>
        /// <returns></returns>
        public List<DepartmentModel> GetDepartment()
        {
            SqlCommand sqlcmd = null;
            List<DepartmentModel> ListOfEmployees = new List<DepartmentModel>();
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.PlainTextCommand(conn, string.Format("SELECT ID, Dept_Name FROM DEPT"));
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DepartmentModel dept = new DepartmentModel();
                            dept.Id = Convert.ToInt16(reader["ID"]);
                            dept.Name = Convert.ToString(reader["Dept_Name"]);
                            ListOfEmployees.Add(dept);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception("An Error Occured");
            }
            return ListOfEmployees;
        }

        /// <summary>
        /// Function the Removes an Employee from the Database
        /// void DeleteEmployee(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        public void DeleteEmployee(int EmployeeId)
        {
            SqlCommand sqlcmd = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_DeleteEmployee");
                    sqlcmd.Parameters.Add("EmployeeId", System.Data.SqlDbType.Int).Value = EmployeeId;
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logger.Error("Error:" + e);
                throw new Exception("An Error Occured");
            }
        }

        /// <summary>
        /// Function that updates the information of a particular Employee
        /// void UpdateEmployeeDetails(EmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEmployeeDetails(EmployeeModel model, string filename)
        {
            SqlCommand sqlcmd = null;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "stp_EditEmployeeDetails");
                    sqlcmd.Parameters.Add("EmployeeId", System.Data.SqlDbType.Int).Value = model.ID;
                    sqlcmd.Parameters.Add("DOB", System.Data.SqlDbType.VarChar).Value = model.DOB;
                    sqlcmd.Parameters.Add("USERNAME", System.Data.SqlDbType.VarChar).Value = model.Username;
                    sqlcmd.Parameters.Add("ADDRESS", System.Data.SqlDbType.VarChar).Value = model.Address;
                    sqlcmd.Parameters.Add("DEPTID", System.Data.SqlDbType.Int).Value = model.Department;
                    sqlcmd.Parameters.Add("SALARY", System.Data.SqlDbType.Int).Value = model.Salary;
                    sqlcmd.Parameters.Add("PASSWORD", System.Data.SqlDbType.VarChar).Value = model.Password;
                    sqlcmd.Parameters.Add("ProfileName", System.Data.SqlDbType.VarChar).Value = filename;
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logger.Error("EmployeeRepository/UpdateEmployeeDetails:" + e.Message);
                throw new Exception(e.Message);
            }
        }

        //public void AddProfileName(string filename)
        //{
        //    SqlCommand sqlcmd = null;
        //    try
        //    {
        //        using (SqlConnection connection = ConnectionManager.GetConnection())
        //        {
        //            sqlcmd = ConnectionManager.GetStoredProcedureCommand(conn, "
                    
        //}
            
    }
}
