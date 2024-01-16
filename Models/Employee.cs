using System.ComponentModel.DataAnnotations;

namespace Empform.Models
{
    public class Employee
    {
        [Key]
        public Guid EmpID { get; set; }
        public string EmpName { get; set; }

        public string EmpEmail { get; set; }

        // public ICollection<Employee> Employees { get; set; }

    }
}
