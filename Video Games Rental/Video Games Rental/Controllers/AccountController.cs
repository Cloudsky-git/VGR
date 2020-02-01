using Video_Games_Rental.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Video_Games_Rental.App_Start;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Linq;
using System.Collections.Generic;


namespace ASPNetIdentity.Controllers
{
    public class AccountController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {        
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    AspNetUserRole role = new AspNetUserRole()
                    {
                        RoleId = "1",
                        UserId = user.Id,                        
                };
                    db.AspNetUserRoles.Add(role);                  
                    db.SaveChanges();
                    
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: ../Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {        
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: ../Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    string userID = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId();
                    string roleID = db.AspNetUserRoles.Where(x => x.UserId == userID).Select(y => y.RoleId).FirstOrDefault();
                    string roleName = db.AspNetRoles.Where(x => x.Id == roleID).Select(y => y.Name).FirstOrDefault();
                    Session["myRole"] = roleName;
                    Session.Remove("Cart");
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: ACCOUNT/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Remove("myRole");
            Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Account_Details()
        {
            return View();
        }

        public ActionResult User_details()
        {
            ViewBag.AspNetUsers_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_details([Bind(Include = "customer_id,AspNetUsers_id,name,surname,address_line1,address_line2,postal_code,Email,PhoneNumber")] customer customer)
        {
            if (ModelState.IsValid)
            {

                customer.AspNetUsers_id = User.Identity.GetUserId();               
                customer.Email = User.Identity.Name;            
                db.customers.Add(customer);
                db.SaveChanges();                
                return RedirectToAction("Account_details");
            }
            

            ViewBag.AspNetUsers_id = new SelectList(db.AspNetUsers, "Id", "Email", customer.AspNetUsers_id);
            return View(customer);
        }            

        public PartialViewResult AspNetUsersList()
        {
            List<customer> custList = db.customers.Where(x => x.Email == User.Identity.Name).ToList();
            if (custList.Count() > 0)
            {
                customer cust = custList.First();
                return PartialView(cust);
            }
            else
            {
                return PartialView();
            }
        }   
    }
}