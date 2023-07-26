using System.ComponentModel.DataAnnotations;

namespace AspNetMVCCRUD.Models
{
    public class Employee
    {
        
        [Key]
        public Guid EmpID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public string Dep { get; set; }
        public string DOB { get; set; }
    }
}
