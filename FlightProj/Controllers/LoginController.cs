using FlightProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;

namespace FlightProj.Controllers{

    public class LoginController: Controller{
        private readonly Ace52024Context db;

        private readonly ISession session;
        

        public LoginController(Ace52024Context _db, IHttpContextAccessor httpContextAccessor){
            db=_db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(Kguser u){
            var result = (from i in db.Kgusers
                          where i.Email==u.Email && i.Password==u.Password && i.UserType=="Admin"
                          select i).SingleOrDefault();
            if(result!=null){
                HttpContext.Session.SetString("UserName", result.Uname);
                return RedirectToAction("ShowBookings", "AdminHome");
            }
            var result1 = (from i in db.Kgusers
                           where i.Email==u.Email && i.Password==u.Password && i.UserType==null
                           select i).SingleOrDefault();
            if(result1!=null){
                HttpContext.Session.SetString("UserName", result1.Uname);
                int customerId = result1.Uid;
                HttpContext.Session.SetString("CustomerId", customerId.ToString());
                return RedirectToAction("UserHomePage", "UserHome");
            }
            
            return View();
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kguser u){
            if(ModelState.IsValid){
                db.Kgusers.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Register","Login");
        }
    }
}