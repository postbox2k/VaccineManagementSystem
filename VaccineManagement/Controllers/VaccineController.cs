using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VaccineManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace VaccineManagement.Controllers
{
    public static class Global
    {
        public static int AAID { get; set; }
    }
    public class VaccineController : Controller
    {
        // creating an object of DbContext class
        
        private DataContext db = new DataContext();
        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag.details = db.Booking.ToList();
            return View();
        }

        //http:Get For Booking
        [HttpGet]
        public IActionResult Book()
        {
            ViewBag.AID = Global.AAID;
            //Console.WriteLine(Global.AAID);
            //Console.WriteLine(ViewBag.AID);
            return View();
        }
        //http:post For Booking
        [HttpPost]
        public IActionResult Book(Booking booking)
        {

            if (ModelState.IsValid)
            {
                db.Booking.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Status");
            }
            return View();
        }
        //Booking Status 
        public IActionResult Status()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BookedDetails()
        {
            ViewBag.details = db.Booking.FromSqlRaw("select * from dbo.Booking where AID=@id", new SqlParameter("@id", Global.AAID)).ToList();
            return View();
        }
        //http get for user creation
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        //http post for user creation
        [HttpPost]
        public IActionResult CreateUser(Account account)
        {

            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }


        //httpGet with Update
       /* [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        //httpPost with Update
        [HttpPost]
        public IActionResult Update(Booking booking)
        {
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BookedDetails");
        }*/

        //GET/booking/Update/Id(1))
       [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BookedDetails"); //new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            Booking booking = db.Booking.Find(id);
            ViewBag.AID = Global.AAID;
            if (booking == null)
            {
                return RedirectToAction("BookedDetails"); //HttpNotFound();
            }
            return View(booking);
        }
        //POST:Vaccine/Update/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Booking booking)
        {
            ViewBag.AID = Global.AAID;
            if (ModelState.IsValid)
            {
                ViewBag.AID = Global.AAID;
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BookedDetails");
            }
            return View(booking);

        }

        //Delete Record 
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BookedDetails"); //new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            Booking booking = db.Booking.Find(id);
            ViewBag.AID = Global.AAID;
            if (booking == null)
            {
                return RedirectToAction("BookedDetails"); //HttpNotFound();
            }
            return View(booking);
        }

     
        //httpPost for delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Booking.Remove(db.Booking.Find(id));
            db.SaveChanges();
            return RedirectToAction("BookedDetails");
        }



//================================================================================
//Login phase
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            ViewBag.creds = db.Account.ToList();
            foreach (var item in ViewBag.creds)
            {
                //need to remove loop

                // ViewBag.details = db.Booking.FromSqlRaw("select * from dbo.Booking where AID=@id", new SqlParameter("@id", Global.AAID)).ToList();
           
            if (username != null && password != null && username.Equals(item.username) && password.Equals(item.password))
            {
                    //store value in session
                    Global.AAID=item.Id;
                   // Console.WriteLine(Global.AAID);
                return RedirectToAction("BookedDetails");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
            }
        }
            return View("Index");
        }
        //Logout Phase
        public IActionResult Logout()
        {
            Global.AAID = -1;
            return RedirectToAction("Index");
        }
    }
}
