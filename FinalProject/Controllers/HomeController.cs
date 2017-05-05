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
            Models.UserModel user = (Models.UserModel)Session["User"];
            if (!(user == null))
            {
                
                List<StudentClass> classes = courseRepository.RetrieveClassesForStudent(user.Id).ToList();
                if (classes.Count()<=0)
                {
                    return View();
                }
                else
                {
                    return View(ConvertToClass(classes).ToArray());
                }              
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
                    return Redirect(returnUrl ?? "~/");
                }
            }
            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (courseRepository.Users.Any(u => u.UserEmail == registerModel.UserName))
            {
                ModelState.AddModelError("UsedUsername", "Username is already taken");
                return View();
            }

            if (ModelState.IsValid)
            {
                userManager.Register(registerModel.UserName, registerModel.Password);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Enroll()
        {
            Models.UserModel user = (Models.UserModel)Session["User"];
            if (!(user == null))
            {
                List<StudentClass> userCourses = courseRepository.RetrieveClassesForStudent(user.Id).ToList();
                var allCourses = courseRepository.Classes.ToList();
                EnrollModel enrollModel = new EnrollModel();

                if (userCourses.Count<=0)
                {
                    enrollModel.Classes = allCourses.ToArray();
                    return View(enrollModel);
                }
                else
                {                    
                    enrollModel.Classes = allCourses.Except(ConvertToClass(userCourses)).ToArray();
                    return View(enrollModel);
                }
                
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        [HttpPost]
        public ActionResult Enroll(EnrollModel enrollModel)
        {
            var index = int.Parse(Request.Form["DropDown"]);
            Class classToAdd = courseRepository.Classes.First(c => c.ClassId == index);
            Models.UserModel user = (Models.UserModel)Session["User"];
            if (!(user == null))
            {
                var userCourses = courseRepository.RetrieveClassesForStudent(user.Id).ToList();
                var dbUser = userManager.GetUser(user.Id);

                dbUser.Classes.Add(classToAdd);
                DatabaseAccessor.Instance.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        private List<Class> ConvertToClass(List<StudentClass> listToConvert)
        {
            List<Class> listToReturn = new List<Class>();
            foreach (StudentClass studentclass in listToConvert)
            {
                Class classToAdd = courseRepository.Classes.First(c => c.ClassId == studentclass.ClassId);
                listToReturn.Add(classToAdd);
            }
            return listToReturn;
        }
    }
}