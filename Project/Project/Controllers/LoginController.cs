using Project.Model.Context;
using Project.Model.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly AlisMatbaaDbContext dbContext = new AlisMatbaaDbContext();
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string mail, string password)
        {
            var encryptedPassword = GetMD5(password);

            User user = dbContext.User.FirstOrDefault(s => s.Mail.Equals(mail) && s.Password.Equals(encryptedPassword));
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

                //add session
                Session["FullName"] = user.Name + " " + user.Surname;
                Session["Email"] = user.Mail;
                Session["UserId"] = user.Id;

                return Redirect("/Order/OrderList");
            }
            else
            {
                TempData["PostResult"] = "Login failed";
                return Redirect("/Login/Index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}