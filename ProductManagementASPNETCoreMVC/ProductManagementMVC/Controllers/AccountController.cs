using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects;
using Microsoft.AspNetCore.Http; // Cần thư viện này để dùng Session

namespace ProductManagementMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var account = _accountService.GetAccountById(email);

            // Kiểm tra tài khoản và mật khẩu
            if (account != null && account.MemberPassword == password)
            {
                // 1. Lưu thông tin vào Session
                HttpContext.Session.SetString("UserEmail", account.EmailAddress);
                HttpContext.Session.SetString("MemberID", account.MemberId);
                HttpContext.Session.SetString("Role", account.MemberRole.ToString());

                return RedirectToAction("Index", "Products");
            }

            // Đăng nhập thất bại
            ViewBag.Error = "Invalid Member ID or Password";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return RedirectToAction("Login", "Account");
        }



    }
}