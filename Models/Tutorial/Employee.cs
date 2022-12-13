using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DefaultDatabase.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId {
            get;
            set;
        }
        public string EmployeeFirstName {
            get;
            set;
        }
        public string EmployeeLastName {
            get;
            set;
        }
        public string Salary {
            get;
            set;
        }
        public string Designation {
            get;
            set;
        }
    }
}
