using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplierRanking.Controllers
{
    public class LoginController : Controller
    {
        // chama tela de login
        public ActionResult Login()
        {
            return View();
        }
    }
}