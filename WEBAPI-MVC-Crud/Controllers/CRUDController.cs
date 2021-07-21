using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBAPI_MVC_Crud.Models;
using System.Net.Http;

namespace WEBAPI_MVC_Crud.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Employee> empobj = null;
            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/");

            var consumeapi = emp.GetAsync("EmpCrud");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Employee>>();
                displaydata.Wait();

                empobj = displaydata.Result;
            }
            return View(empobj);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Create(Employee insertemp)
        {
            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/EmpCrud");

            var insertrecord = emp.PostAsJsonAsync<Employee>("EmpCrud", insertemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View("create");

        } 
        public ActionResult Details(int Id)
        {
            EmpClass empobj = null;

            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/");

            var consumeapi = emp.GetAsync("EmpCrud?Id=" + Id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        public ActionResult Edit(int Id)
        {
            EmpClass empobj = null;

            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/");

            var consumeapi = emp.GetAsync("EmpCrud?Id=" + Id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        [HttpPost]
        public ActionResult Edit(EmpClass empup)
        {
            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/EmpCrud");
            var insertrecord = emp.PutAsJsonAsync<EmpClass>("EmpCrud", empup);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.message = "Employee Record Not Update ...!";
            }
            return View(empup);
        }
        
        public ActionResult Delete(int Id)
        {
            HttpClient emp = new HttpClient();
            emp.BaseAddress = new Uri("https://localhost:44394/api/EmpCrud");

            var delrecord = emp.DeleteAsync("EmpCrud/" + Id.ToString());

                delrecord.Wait();

            var displaydata = delrecord.Result;
            if(displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}