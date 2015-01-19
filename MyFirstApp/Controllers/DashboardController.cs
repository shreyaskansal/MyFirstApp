using Core.Models;
using log4net;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyFirstApp.Controllers
{
    public class DashboardController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string filename;

        private EmployeeService service { get; set; }

        public DashboardController()
        {
            service = new EmployeeService();
        }

        public ActionResult Index()
        {
            ResultList<Core.Models.EmployeeModel> getlist = new ResultList<Core.Models.EmployeeModel>();
            try
            {
                getlist.list = service.GetEmployeeList();
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return View(getlist);
        }

        /// <summary>
        /// Display Employee List using Pagination
        /// Search Employee Name in the database
        /// JsonResult JsonEmployeeList(int Offset, int PageSize, string SearchEmployeeName)
        /// </summary>
        [HttpPost]
        public JsonResult JsonEmployeeList(int Offset, int PageSize, string SearchEmployeeName = "")
        {
            try
            {
                return Json(service.JsonUser(Offset, PageSize, SearchEmployeeName));
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.StackTrace);
            }
        }

        /// <summary>
        /// Action that updates the information of a particular Employee
        /// ActionResult EmployeeDetails(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public ActionResult EmployeeDetails(int EmployeeId)
        {
            EmployeeModel employee = new EmployeeModel();
            try
            {
                employee = service.EmployeeDetails(EmployeeId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return PartialView("_EmployeeDetails", employee);
        }

        /// <summary>
        /// Action that displays the list of the department 
        /// ActionResult Department()
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public ActionResult Department()
        {
            ResultList<Core.Models.DepartmentModel> department = new ResultList<Core.Models.DepartmentModel>();
            try
            {
                department = service.Department();
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.StackTrace);
            }
            return PartialView("_DepartmentView", department);
        }

        /// <summary>
        /// Display Department List using Pagination
        /// Search Depaartment Name in the database
        /// JsonResult JsonDepartment(int Offset, int PageSize, string SearchDepartmentName = "")
        /// </summary>
        [HttpPost]
        public JsonResult JsonDepartment(int Offset, int PageSize, string SearchDepartmentName = "")
        {
            ResultList<DepartmentModel> model = null;
            try
            {
                model = service.JsonDepartment(Offset, PageSize, SearchDepartmentName);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return Json(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public JsonResult DepartmentDetails(int DepartmentId)
        {
            ResultList<DepartmentModel> model = new ResultList<DepartmentModel>();
            try
            {
                model = service.DepartmentDetails(DepartmentId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.StackTrace);
            }
            return Json(model);
        }

        /// <summary>
        /// Action that posts an Employee Form to add in the Database
        /// ActionResult AddEmployeeForm()
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEmployeeForm()
        {
            try
            {
                SelectList deptselect = new SelectList(service.GetDepartment(), "Id", "Name");
                ViewBag.dept = deptselect;
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return View();
        }

        /// <summary>
        /// Action that passes all the arguments in the form of a model
        /// and updates the database
        /// ActionResult AddEmployee(AddEmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEmployee(AddEmployeeModel model)
        {
            try
            {
                // if (ModelState.IsValid)
                // {
                service.Addemployee(model);
                return RedirectToAction("Index", "Dashboard");
                //}
                //else
                //   return View("AddEmployeeForm",model);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Action that removes an Employee from the Database
        /// ActionResult DeleteEmployee(int EmployeeId)
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public ActionResult DeleteEmployee(int EmployeeId)
        {
            try
            {
                service.DeleteEmployee(EmployeeId);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// JsonResult ProfileUpload()
        /// </summary>
        /// <returns></returns>
        public JsonResult ProfileUpload()
        {
            if (Request.Files != null && Request.Files.Count != 0)
            {
                string profile = ConfigurationSettings.AppSettings["ProfilePicturePath"];
                filename = Request.Files[0].FileName;
                Request.Files[0].SaveAs(profile + filename);
            }
            return this.Json(filename);
        }

        /// <summary>
        /// Action that Updates the details of a particular Employee
        /// ActionResult UpdateEmployeeDetails(EmployeeModel model)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateEmployeeDetails(EmployeeModel model)
        {
           //string filename="empty.jpg";
            try
            {
                service.UpdateEmployeeDetails(model, filename);
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.Message);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// Action that post an Update Employee Information Form
        /// ActionResult UpdateEmployeeForm(int id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateEmployeeForm(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            try
            {
                employee = service.EmployeeDetails(id);
                SelectList deptselect = new SelectList(service.GetDepartment(), "Id", "Name");
                ViewBag.dept = deptselect;
            }
            catch (Exception e)
            {
                logger.Error("Error in Index function" + e);
                throw new Exception(e.StackTrace);
            }
            return View(employee);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        
    }
}