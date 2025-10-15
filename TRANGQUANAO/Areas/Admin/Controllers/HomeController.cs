using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TRANGQUANAO.Models;
using TRANGQUANAO.ViewModels;

namespace TRANGQUANAO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminBaseController
    {
        private readonly TrangquanaoContext _context;

        public HomeController(TrangquanaoContext context)
        {
            _context = context;
        }

        // GET: Admin/Home/Index
        public async Task<IActionResult> Index()
        {
            var vm = new AdminDashboardViewModel
            {
                TotalProducts = await _context.HangHoas.CountAsync(),
                TotalOrders = await _context.HoaDons.CountAsync(),
                TotalCustomers = await _context.KhachHangs.CountAsync(),
                TotalSuppliers = await _context.NhaCungCaps.CountAsync(),
                TotalEmployees = await _context.NhanViens.CountAsync(),
                RecentProducts = await _context.HangHoas
                                    .Include(h => h.MaNccNavigation)
                                    .Include(h => h.MaLoaiNavigation)
                                    .OrderByDescending(h => h.MaHh)
                                    .Take(6)
                                    .ToListAsync()
            };

            return View(vm);
        }
    }
}
