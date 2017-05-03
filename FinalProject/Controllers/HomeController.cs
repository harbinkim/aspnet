using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {

        private SchoolEntities courseRepository;
        private readonly IUserManager userManager;
        private UserManagerModel userModel;

        public HomeController()
        {

        }

        public HomeController(SchoolEntities courseRepository, UserManager usermanager)
        {
            this.courseRepository = DatabaseAccessor.Instance;
            this.userManager = usermanager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Course()
        {
            return View(courseRepository.Classes.First());
        }

        public ActionResult Courses()
        {

            return View(courseRepository.Classes.ToArray());
        }

        public ActionResult UserCourses()
        {
            if (!(userModel == null))
            {
                var classes = courseRepository.RetrieveClassesForStudent(userModel.Id);
                return View(classes);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
            
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match any record");
                }
                else
                {
                    Session["User"] = new Models.UserModel { Id = user.Id, Name = user.Name };
                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);
                    userModel = user;
                    return Redirect(returnUrl ?? "~/");
                }
            }
            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }
    }
}