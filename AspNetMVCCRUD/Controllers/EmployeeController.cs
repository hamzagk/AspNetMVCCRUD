using AspNetMVCCRUD.Data;
using AspNetMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace AspNetMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDBContext mVCDBContext;
        // changes in github
        // to save entity to the database create constructor class
        public EmployeeController(MVCDBContext mVCDBContext)
        {
           
            this.mVCDBContext = mVCDBContext;
        }

        // list showing to the user
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees=await mVCDBContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
       // saving the data
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            // do conversion
            var employee = new Employee()
            {
                EmpID = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Dep = addEmployeeRequest.Dep,
                DOB = addEmployeeRequest.DOB
                
            };
           await mVCDBContext.Employees.AddAsync(employee);
           await mVCDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // view the details of employee facing issue in the View
        [HttpGet]
        // error solved by using FirstOrDefault Instead of FristOrDefaultAsync using no async
        public async Task<IActionResult> View(Guid Id)
        {
            var employees =await mVCDBContext.Employees.FirstOrDefaultAsync(x => x.EmpID == Id);
            // fetching the single employee
            if(employees != null) {
                var viewModel = new updateemployeeViewModel()
                {
                    EmpID = employees.EmpID,
                    Name = employees.Name,
                    Email = employees.Email,
                    Salary = employees.Salary,
                    Dep = employees.Dep,
                    DOB = employees.DOB
                };
                // to solve the error we use await Task.Run()
                return await Task.Run(()=>View("View",viewModel));
            }
            return RedirectToAction("Index");
        }
        // funcationality for the update method now post
        [HttpPost]
        public async Task<IActionResult> View(updateemployeeViewModel model)
        {
            var employee=await mVCDBContext.Employees.FindAsync(model.EmpID);
            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Dep = model.Dep;
                employee.DOB = model.DOB;
                await mVCDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // delete Functionality
        [HttpPost]
        public async Task<IActionResult> Delete(updateemployeeViewModel model)
        {
            var employee = await mVCDBContext.Employees.FindAsync(model.EmpID);
            if (employee != null)
            {
                mVCDBContext.Employees.Remove(employee);
                await mVCDBContext.SaveChangesAsync();
                return RedirectToAction("View");
            }
            return RedirectToAction("Index");
        }
         
}
}
