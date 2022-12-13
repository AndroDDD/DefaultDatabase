using DefaultDatabase.Models;
using DefaultDatabase.ViewModels;
using DefaultDatabase.DbContexts;

namespace DefaultDatabase.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// get list of all employees
        /// </summary>
        /// <returns></returns>
        List<Employee> GetEmployeesList();

        /// <summary>
        /// get employee details by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Employee GetEmployeeDetailsById(int empId);

        /// <summary>
        /// add edit employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns><returns>
        ResponseModel SaveEmployee(Employee employeeModel);

        /// <summary>
        /// delete employees
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        ResponseModel DeleteEmployee(int employeeId);

    }

    public class EmployeeService : IEmployeeService {
        private DefaultContext _context;
        public EmployeeService(DefaultContext context){
            _context = context;
        }

        /// <summary>
        /// get list of all employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployeesList() {
            List<Employee> empList;
            try {
                empList = _context.Set<Employee>().ToList();
            } catch (Exception){
                throw;
            }
            return empList;
        }

        /// <summary>
        /// get employee details by employee id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public Employee GetEmployeeDetailsById(int empId) {
            Employee emp;
            try {
                emp = _context.Find<Employee>(empId);
            } catch {
                throw;
            }
            return emp;
        }

        /// <summary>
        /// add edit employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public ResponseModel SaveEmployee(Employee employeeModel) {
            ResponseModel model = new ResponseModel();
            try {
                Employee _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
                if(_temp != null){
                    _temp.Designation = employeeModel.Designation;
                    _temp.EmployeeFirstName = employeeModel.EmployeeFirstName;
                    _temp.EmployeeLastName = employeeModel.EmployeeLastName;
                    _temp.Salary = employeeModel.Salary;
                    _context.Update<Employee>(_temp);
                    model.Message = "Employee Update Successfully";
                } else {
                    _context.Add<Employee>(employeeModel);
                    model.Message = "Employee Inserted Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;
            } catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// delete employees
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public ResponseModel DeleteEmployee(int employeeId) {
            ResponseModel model = new ResponseModel();
            try {
                Employee _temp = GetEmployeeDetailsById(employeeId);
                if (_temp != null){
                    _context.Remove<Employee>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Employee Deleted Successfully";
                } else {
                    model.IsSuccess = false;
                    model.Message = "Employee Not Found";
                }
            } catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}