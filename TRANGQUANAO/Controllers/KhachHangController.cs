
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TRANGQUANAO.Helpers;
using TRANGQUANAO.Models;
using TRANGQUANAO.ViewModels;

namespace TRANGQUANAO.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly TrangquanaoContext db;
        private readonly IMapper _mapper;

        public KhachHangController(TrangquanaoContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRamdomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.Hieuluc = true;//sẽ xử lý khi dùng Mail để active
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }

                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }
        #endregion


        #region Login
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);

                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");
                    return View(model);
                }

                if (!khachHang.Hieuluc)
                {
                    ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
                    return View(model);
                }

                // ✅ Kiểm tra mật khẩu
                var matKhauMaHoa = model.Password.ToMd5Hash(khachHang.RandomKey);
                if (khachHang.MatKhau?.ToUpper() != matKhauMaHoa.ToUpper())
                {
                    ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                    return View(model);
                }

                // ✅ Tạo role
                string role = khachHang.VaiTro == 1 ? "Admin" : "Customer";

                // ✅ Lưu claims (cho cookie login)
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, khachHang.Email ?? ""),
            new Claim(ClaimTypes.Name, khachHang.HoTen ?? ""),
            new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh ?? ""),
            new Claim(ClaimTypes.Role, role)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                // ✅ Lưu session (cho AdminBaseController)
                HttpContext.Session.SetString("VaiTro", role);

                Console.WriteLine($"Đăng nhập thành công - Vai trò: {role}");

                // ✅ Nếu là admin → vào Admin area
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                // ✅ Nếu có ReturnUrl → quay lại đó
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                // ✅ Ngược lại → về trang chủ
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        #endregion



        #region Quên mật khẩu
        [HttpGet, AllowAnonymous]
        public IActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult QuenMatKhau(QuenMatKhauVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm khách hàng theo email
            var kh = db.KhachHangs.SingleOrDefault(k => k.Email == model.Email);
            if (kh == null)
            {
                ViewBag.Error = "Không tìm thấy tài khoản với email này.";
                return View(model);
            }   

            // Tạo mật khẩu mới ngẫu nhiên (8 ký tự)
            string newPass = Guid.NewGuid().ToString().Substring(0, 8);

            // Mã hoá theo RandomKey cũ
            kh.MatKhau = newPass.ToMd5Hash(kh.RandomKey);
            db.Update(kh);
            db.SaveChanges();

            // Gửi email
            string subject = "Cấp lại mật khẩu - Hệ thống quần áo";
            string body = $"Xin chào {kh.HoTen},\n\nMật khẩu mới của bạn là: {newPass}\nVui lòng đăng nhập và đổi mật khẩu sau khi vào hệ thống.\n\nTrân trọng,\nHệ thống quần áo";

            EmailHelper.SendEmail(kh.Email, subject, body);

            ViewBag.Success = "Mật khẩu mới đã được gửi tới email của bạn.";
            return View();
        }
        #endregion







        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            // Xóa cookie đăng nhập (nếu dùng cookie-based auth)
            await HttpContext.SignOutAsync();

            // Xóa session nếu có lưu vai trò hoặc thông tin user
            HttpContext.Session.Clear();

            // Chuyển hướng về trang đăng nhập của khách hàng
            return RedirectToAction("DangNhap", "KhachHang", new { area = "" });
        }



        // ------------------ CHÍNH SÁCH VÀ HƯỚNG DẪN ------------------

        public IActionResult ChinhSachDoiTra()
        {
            return View();
        }

        public IActionResult ChinhSachBaoHanh()
        {
            return View();
        }

        public IActionResult HuongDanChonSize()
        {
            return View();
        }
    }
}
