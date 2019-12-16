using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTrackingServices.Models
{
    public class EmployeesRepository
    {
        private static ProjectTrackingDBEntities dbentity = new ProjectTrackingDBEntities();

        public static List<Employee> GetAllEmployees()
        {
            return dbentity.Employees.ToList<Employee>();
        }

        public static List<Employee> GetEmployeeByName(string name)
        {
            var query = from emp in dbentity.Employees
                        where emp.EmployeeName.Contains(name)
                        select emp;
            return query.ToList();
        }

        public static List<Employee> GetEmployeesByID(int empid)
        {
            var query = from emp in dbentity.Employees
                        where emp.EmployeeID == empid
                        select emp;
            return query.ToList();
        }

        public static List<Employee> InsertEmployee(Employee e)
        {
            dbentity.Employees.Add(e);
            dbentity.SaveChanges();

            return GetAllEmployees();

        }

        public static List<Employee> UpdateEmployee(Employee e)
        {
            Employee empobj = (from emp in dbentity.Employees
                               where emp.EmployeeID == e.EmployeeID
                               select emp).SingleOrDefault<Employee>();

            empobj.EmployeeName = e.EmployeeName;
            empobj.ContactNo = e.ContactNo;
            empobj.Designation = e.Designation;
            empobj.EMailID = e.EMailID;
            empobj.SkillSets = e.SkillSets;

            dbentity.SaveChanges();

            return GetAllEmployees();
        }

        public static List<Employee> DeleteEmployee(Employee e)
        {
            Employee empobj = (from emp in dbentity.Employees
                               where emp.EmployeeID == e.EmployeeID
                               select emp).SingleOrDefault<Employee>();

            dbentity.Employees.Remove(empobj);
            dbentity.SaveChanges();

            return GetAllEmployees(); 
        }
    }
}