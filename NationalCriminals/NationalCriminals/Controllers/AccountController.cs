using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using NationalCriminals.Models;

namespace NationalCriminals.Controllers
{
    public class AccountController : Controller
    {
        NationalCriminalsEntities db;

        public AccountController()
        {
            db = new NationalCriminalsEntities();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Allows user to login into application.
        /// </summary>
        /// <param name="model">object of login view model.</param>
        /// <returns>View of login.</returns>        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = db.Users.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                Session["FirstName"] = user.FirstName;
                Session["LastName"] = user.LastName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is invalid.");
                return View();
            }

        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var data = db.Users.FirstOrDefault(a => a.Email == model.Email);

            if (data != null)
            {
                ModelState.AddModelError("", "Email is already registered. Please try another one.");
                return View();
            }
            else
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    IsDeleted = false,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();                

                Session["UserId"] = user.UserId;
                Session["FirstName"] = user.FirstName;
                Session["LastName"] = user.LastName;

                return RedirectToAction("Index", "Home");
            }

        }

    }

}
