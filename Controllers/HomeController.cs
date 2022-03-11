using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Result"] = Session["userName"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
            {
                ModelState.AddModelError(nameof(user.Login), "Введите имя");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError(nameof(user.Password), "Введите пароль");
                return View(user);
            }
            if (user.Password.Length < 8)
            {
                ModelState.AddModelError(nameof(user.Password), "Длина пароля не менее 8 символов");
            }
            if (string.IsNullOrEmpty(user.СheckPassword))
            {
                ModelState.AddModelError(nameof(user.СheckPassword), "Введите пароль");
            }
            if (user.Password != user.СheckPassword)
            {
                ModelState.AddModelError(nameof(user.Password), "Пароли не совпадают");
            }


            if (string.IsNullOrEmpty(user.Email))
            {
                ModelState.AddModelError(nameof(user.Email), "Введите email");
                return View(user);
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(user.Email);
            if (!match.Success)
            {
                ModelState.AddModelError(nameof(user.Email), "Недопустимый формат почты");
            }
            if (string.IsNullOrEmpty(user.Age))
            {
                ModelState.AddModelError(nameof(user.Age), "Введите возраст");
                return View(user);
            }
            int age = 0;
            if (!Int32.TryParse(user.Age, out age))
            {
                ModelState.AddModelError(nameof(user.Age), "Введены буквы");
                return View(user);
            }
            if (Int32.Parse(user.Age) < 18 || Int32.Parse(user.Age) > 65)
            {
                ModelState.AddModelError(nameof(user.Age), "Допустимый возрастной диапазрн от 18 до 65 лет");
            }

            Session["userName"] = "Логин: " + user.Login + ". Пароль: " + user.Password;

            if (ModelState.IsValid)
            {
                return Redirect("~/Home/Contact");
            }
            else
            {
                return View(user);
            }
        }
    }
}