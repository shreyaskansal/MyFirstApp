using Core.IContracts;
using Core.Models;
using Persistance;
using System;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public class EmployeeService : IEmployeeService
    {
        private static readonly ILog logger =
          LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EmployeeRepository repository {get;set;}

        public EmployeeService()
        {
            repository = new EmployeeRepository();
        }

       

        /// <summary>
        /// Get All the Employee List
        /// List<EmployeeModel> GetEmployeeList()
        /// </summary>
        /// <returns></returns>
        public List<EmployeeModel> GetEmployeeList()
        {
            try
            {
                return repository.GetEmployeeList();
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
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
            try
            {
                return repository.JsonUser(Offset, PageSize, SearchEmployeeName);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get the Details of a particular Employee
        /// EmployeeModel EmployeeDetails(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public EmployeeModel EmployeeDetails(int EmployeeId)
        {
            try
            {
                return repository.EmployeeDetails(EmployeeId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Get Department List
        /// DepartmentModel Department()
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public ResultList<Core.Models.DepartmentModel> Department()
        {
            try
            {
                return repository.Department();
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get the Department List using Pagination
        /// Search Department Name in the database
        /// ResultList<DepartmentModel> JsonUser(int Offset, int PageSize, string SearchDepartmentName)
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="PageSize"></param>
        /// <param name="SearchEmployeeName"></param>
        /// <returns></returns>
        public ResultList<DepartmentModel> JsonDepartment(int Offset, int PageSize, string SearchDepartmentName)
        {
            try
            {
                return repository.JsonDepartment(Offset, PageSize, SearchDepartmentName);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public ResultList<DepartmentModel> DepartmentDetails(int DepartmentId)
        {
            try
            {
                return repository.DepartmentDetails(DepartmentId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Function to add an Employee in the Database
        /// void AddEmployeemodel(AddEmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        public void Addemployee(AddEmployeeModel model)
        {
            try
            {
                repository.AddEmployeemodel(model);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Function that returns the model of Dept Table with DeptId and DeptName
        /// List<Department> GetDepartment()
        /// </summary>
        /// <returns></returns>
        public List<DepartmentModel> GetDepartment()
        {
            try
            {
                return repository.GetDepartment();
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Function the Removes an Employee from the Database
        /// void DeleteEmployee(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        public void DeleteEmployee(int EmployeeId)
        {
            try
            {
                repository.DeleteEmployee(EmployeeId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Function that updates the information of a particular Employee
        /// void UpdateEmployeeDetails(EmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEmployeeDetails(EmployeeModel model, string filename)
        {
            try
            {
                repository.UpdateEmployeeDetails(model, filename);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        //public void AddProfileName(string filename)
        //{
        //    try
        //    {
        //        repository.AddProfileName(filename);
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("Error in service AddProfileName" + e);
        //        throw new Exception(e.Message);
        //    }
        //}


        public void UpdateEmployeeDetails(EmployeeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
