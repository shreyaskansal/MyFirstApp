using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.IContracts
{
     [ServiceContract]
     public interface IEmployeeService
    {
       [OperationContract]
         List<EmployeeModel> GetEmployeeList();
       [OperationContract]
         List<DepartmentModel> GetDepartment();
       [OperationContract]
       void Addemployee(AddEmployeeModel model);
       [OperationContract]
       void DeleteEmployee(int EmployeeId);
       [OperationContract]
       void UpdateEmployeeDetails(EmployeeModel model);
    }
}
