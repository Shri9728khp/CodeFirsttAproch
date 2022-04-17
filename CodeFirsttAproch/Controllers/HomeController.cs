using CodeFirsttAproch.Models;
using CodeFirsttAproch.My_Folder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeFirsttAproch.Controllers
{
    public class HomeController : Controller
    {
        private readonly Application_dbcontext _db;
       

        public HomeController(Application_dbcontext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var res = _db.Employees.ToList();
            return View(res);


            
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(

              CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginForm");
        }
        

        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginForm(LodinClass obj)
        {

            var log = _db.LodinClass.Where(m => m.Email == obj.Email).FirstOrDefault();
            if (log == null)
            {
                TempData["Email"] = "Email invalid";
            }
            else
            {
                if (log.Email == obj.Email && log.Password == obj.Password)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, log.Name),
                                        new Claim(ClaimTypes.Email,log.Email) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    authProperties);

                    HttpContext.Session.SetString("Name", log.Email);


                    return RedirectToAction("Index");

                }

                else
                {

                }


            }


            return View("Index");
        }
        [HttpGet]

        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]


        public IActionResult Form(Employee obj)
        {
           
            Employee obj2 = new Employee();

            obj2.Id = obj.Id;
            obj2.Name = obj.Name;
            obj2.Rollno = obj.Rollno;
            obj2.Email = obj.Email;
            obj2.Mobile = obj.Mobile;
            obj2.Address = obj.Address;
            obj2.Gender = obj.Gender;
          
            if (obj.Id == 0)
            {
                _db.Employees.Add(obj2);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                _db.Entry(obj2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult edit(int Id)
        {
            Employee obj2 = new Employee();

            Employee obj = new Employee();
            var edit = _db.Employees.Where(m => m.Id == Id).FirstOrDefault();
            obj2.Id = edit.Id;
            obj2.Name = edit.Name;
            obj2.Rollno = edit.Rollno;
            obj2.Email = edit.Email;
            obj2.Mobile = edit.Mobile;
            obj2.Address = edit.Address;
            obj2.Gender = edit.Gender;
        
            return View("form", obj2);





        }
        public IActionResult Delete(int Id)
        {
            Employee obj = new Employee();
            var del = _db.Employees.Where(m => m.Id == Id).FirstOrDefault();

            _db.Employees.Remove(del);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
