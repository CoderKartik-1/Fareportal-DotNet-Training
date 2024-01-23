using Microsoft.AspNetCore.Mvc;
using FlightProj.Models;

namespace FlightProj.Controllers;

public class PrivacyController : Controller
{   
    public ActionResult CustomerService(){
        return View();
    }

    public ActionResult Privacy(){
        return View();
    }
}