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
            // Kiểm tra thông tin đăng nhập qua Service
            var account = _accountService.GetAccountById(email); // Lưu ý: Trong AccountDAO bạn đang dùng ID là Email hoặc MemberID, hãy kiểm tra lại logic này trong DAO nhé.

            // Giả sử trong Lab này MemberID chính là Email hoặc bạn nhập ID vào ô User
            if (account != null && account.MemberPassword == password) // So sánh password (trong thực tế nên mã hóa)
            {
                // Mật khẩu đúng -> Lưu thông tin vào Session
                HttpContext.Session.SetString("UserEmail", account.EmailAddress);
                HttpContext.Session.SetString("Role", account.MemberRole.ToString());

                // Chuyển hướng sang trang quản lý sản phẩm
                return RedirectToAction("Index", "Products");
            }

            // Mật khẩu sai
            ViewBag.Error = "Invalid email or password";
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