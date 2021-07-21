using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPI_MVC_Crud.Models;

namespace WEBAPI_MVC_Crud.Controllers
{
    public class EmpCrudController : ApiController
    {
        CRUDDBEntities db = new CRUDDBEntities();
        public IHttpActionResult getemp()
        {

            var results = db.Employees.ToList();
            return Ok(results);
        }
        [HttpPost]
        public IHttpActionResult empinsert(Employee empinsert)
        {
            db.Employees.Add(empinsert);
            db.SaveChanges();
            return Ok();
          }
        public IHttpActionResult GetEmployeeID(int Id)
        {
            EmpClass empdetails = null;
            empdetails = db.Employees.Where(x => x.EmployeeID == Id).Select(x => new EmpClass()
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                Position = x.Position,
                Age = x.Age,
                Salary = x.Salary,
            }).FirstOrDefault<EmpClass>();
            if(empdetails==null)
            {
                return NotFound();
            }
            return Ok(empdetails);
        }



        public IHttpActionResult Put(EmpClass empup)
        {
            var updateemp = db.Employees.Where(x => x.EmployeeID == empup.EmployeeID).FirstOrDefault<Employee>();
            if (updateemp != null)
            {
                updateemp.EmployeeID = empup.EmployeeID;
                updateemp.Name = empup.Name;
                updateemp.Position = empup.Position;
                updateemp.Age = empup.Age;
                updateemp.Salary = empup.Salary;
                db.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();  
        }
        
        public IHttpActionResult Delete(int Id )
        {
            var empdel = db.Employees.Where(x => x.EmployeeID == Id).FirstOrDefault();
            db.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
