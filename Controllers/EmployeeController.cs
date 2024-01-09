using ClosedXML.Excel;
using DapperMVC.Models;
using DapperMVC.Repo;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DapperMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeRepo _employeeRepo;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertData(Employee emp)
        {
            string s = "";
            foreach (var item in emp.AddSkill)
            {
                s = item + "," + s;
            }
            // tring s1 = s.Length - 1;
            s = s.Substring(0, s.Length - 1);
            emp.Skill = s;
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            _employeeRepo.InsertEmployee(emp);
            return View(emp);
        }
        public ActionResult AllEmployee(int? page, int pageSize = 2)
        {
            int pageNumber = (page ?? 1);
            ViewBag.PageSize = pageSize;
            ViewBag.PagesizeList = new SelectList(new[] { 2,5,10},pageSize);
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            var data = _employeeRepo.GetAllEmployee().ToList().ToPagedList(pageNumber, pageSize);
            //ViewBag.PageSize = pageSize;
            return View(data);
        }
        public ActionResult AllDeleteData()
        {
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            var data = _employeeRepo.DeleteAllEmployee();
            return View(data);
        }

        public ActionResult UpdateView(int id)
        {
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            var employee = _employeeRepo.GetByEmployeeID(id);
            return View(employee);
        }

        public ActionResult UpdateData(Employee employee)
        {
            string s = "";
            foreach (var item in employee.AddSkill)
            {
                s = item + "," + s;
            }
            // tring s1 = s.Length - 1;
             s=s.Substring(0, s.Length - 1);
            employee.Skill = s;
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            _employeeRepo.UpdateEmployee(employee);
            return RedirectToAction("AllEmployee", "Employee");

        }

        public ActionResult Delete(int id)
        {

            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            _employeeRepo.DeleteEmployee(id);
            return RedirectToAction("AllEmployee", "Employee");
        }

        public ActionResult Restore(int id)
        {
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            _employeeRepo.ActiveEmployee(id);
            return RedirectToAction("AllDeleteData", "Employee");
        }

        public ActionResult ExcelPrintActiveEmployee()
        {
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            List<Employee> sampleList = _employeeRepo.GetAllEmployee().ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SampleData");
                var firstObject = sampleList[0];
                var properties = firstObject.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }
                for (int row = 0; row < sampleList.Count; row++)
                {
                    var rowData = sampleList[row];
                    for (int col = 0; col < properties.Length; col++)
                    {
                        worksheet.Cell(row + 2, col + 1).Value = properties[col].GetValue(rowData, null)?.ToString();
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
                }
            }
        }

        public ActionResult ExcelPrintInActiveEmployee()
        {
            _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
            List<Employee> sampleList = _employeeRepo.DeleteAllEmployee().ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SampleData");
                var firstObject = sampleList[0];
                var properties = firstObject.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }
                for (int row = 0; row < sampleList.Count; row++)
                {
                    var rowData = sampleList[row];
                    for (int col = 0; col < properties.Length; col++)
                    {
                        worksheet.Cell(row + 2, col + 1).Value = properties[col].GetValue(rowData, null)?.ToString();
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
                }
            }
        }
        public ActionResult SortName(string sortName)
        {

            try
            {
                _employeeRepo = new EmployeeRepo("Data Source=DESKTOP-SQ43BP0;Initial Catalog=EmployeeDB;User ID=sa ; Password=123");
                List<Employee> sampleList = _employeeRepo.GetAllEmployee().ToList();
                var employee = from emp in sampleList select emp;
                switch (sortName)
                {
                    case "Id":
                        employee = employee.OrderBy(emp => emp.Id);
                        break;
                    case "Name":
                        employee = employee.OrderBy(emp => emp.Name);
                        break;
                    case "Email":
                        employee = employee.OrderBy(emp => emp.Email);
                        break;
                    case "Phone":
                        employee = employee.OrderBy(emp => emp.Phone);
                        break;
                    case "Gender":
                        employee = employee.OrderBy(emp => emp.Gender);
                        break;
                    case "Skill":
                        employee = employee.OrderBy(emp => emp.Skill);
                        break;
                    default:

                        break;
                }
                var jsonResult = Json(employee, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error in SortName action: {ex.Message}");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }




    }
}
