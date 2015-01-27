using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DepartmentModel : User
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int EmployeeCount { get; set; }
        public List<EmployeeModel> elist { get; set; }
    }
}
