using AspNetMVCCRUD.Data;
using AspNetMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDBContext mVCDBContext;

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
        public async Task<ActionResult> View(Guid Id)
        {
            // fetching the single employee
           var employee= mVCDBContext.Employees.FirstOrDefaultAsync(x => x.EmpID == Id);
            var viewModel = new updateemployeeViewModel()
     {
                 

                };
            return View(employee);
        }
    }
}
