
using Microsoft.AspNetCore.Mvc;
using FlightProj.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightProj.Controllers;

public class AdminHomeController : Controller
{   
    private readonly Ace52024Context db;

        private readonly ISession session;

        public AdminHomeController(Ace52024Context _db, IHttpContextAccessor httpContextAccessor){
            db=_db;
            session = httpContextAccessor.HttpContext.Session;
        }

    public ActionResult ShowBookings(){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            return View(db.Kgbookings.Include(y=>y.UidNavigation).Include(z=>z.FidNavigation).ToList());
        }
        return RedirectToAction("Login", "Login");
    }

    public ActionResult AdminDeleteBooking(int id){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            Kgbooking b = db.Kgbookings.Where(x=>x.BookingId == id).SingleOrDefault();
            return View(b);
        }
        return RedirectToAction("Login", "Login");
    }

    [HttpPost]
    [ActionName("AdminDeleteBooking")]
    public ActionResult AdminDeleteBookingConfirmed(int id){
        Kgbooking b = db.Kgbookings.Include(y=>y.UidNavigation).Include(z=>z.FidNavigation).Select(x=>x).Where(x=>x.BookingId == id).SingleOrDefault();
        db.Kgbookings.Remove(b);
        db.SaveChanges();
        return RedirectToAction("ShowBookings");
    }
}
