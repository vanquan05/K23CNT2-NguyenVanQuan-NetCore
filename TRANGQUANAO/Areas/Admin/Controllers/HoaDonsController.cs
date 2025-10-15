using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRANGQUANAO.Models;
using TRANGQUANAO.ViewModels;

namespace TRANGQUANAO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonsController : AdminBaseController
    {
        private readonly TrangquanaoContext _context;

        public HoaDonsController(TrangquanaoContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDons
        public async Task<IActionResult> Index()
        {
            var trangquanaoContext = _context.HoaDons.Include(h => h.MaKhNavigation).Include(h => h.MaNvNavigation);
            return View(await trangquanaoContext.ToListAsync());
        }

        // GET: Admin/HoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,NgayDat,NgayCan,NgayGiao,HoTen,DiaChi,DienThoai,CachThanhToan,CachVanChuyen,PhiVanChuyen,MaTrangThai,MaNv,GhiChu")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(hoaDon);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Ghi log ra Output Window
                    Console.WriteLine("==== CREATE ERROR ====");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("==== MODELSTATE INVALID ====");
                foreach (var e in ModelState)
                {
                    Console.WriteLine($"{e.Key} -> {string.Join(", ", e.Value.Errors.Select(er => er.ErrorMessage))}");
                }
            }

            // Load lại danh sách combobox nếu lỗi
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoaDon.MaNv);

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoaDon.MaNv);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHd,MaKh,NgayDat,NgayCan,NgayGiao,HoTen,DiaChi,DienThoai,CachThanhToan,CachVanChuyen,PhiVanChuyen,MaTrangThai,MaNv,GhiChu")] HoaDon hoaDon)
        {
            if (id != hoaDon.MaHd)
                return NotFound();

            if (!ModelState.IsValid)
            {
                Console.WriteLine("===== MODELSTATE INVALID =====");
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var e in errors)
                    {
                        Console.WriteLine($"{key} -> {e.ErrorMessage}");
                    }
                }

                ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
                ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoaDon.MaNv);
                return View(hoaDon);
            }

            try
            {
                _context.Update(hoaDon);
                await _context.SaveChangesAsync();
                Console.WriteLine("==== ĐÃ LƯU THÀNH CÔNG ====");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("==== LỖI KHI LƯU ====");
                Console.WriteLine(ex.Message);
            }

            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", hoaDon.MaKh);
            ViewData["MaNv"] = new SelectList(_context.NhanViens, "MaNv", "MaNv", hoaDon.MaNv);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                TempData["Error"] = "Không tìm thấy hóa đơn cần xóa!";
                return RedirectToAction(nameof(Index));
            }

            // ✅ Kiểm tra xem có chi tiết hóa đơn nào đang dùng không
            bool coChiTiet = await _context.ChiTietHds.AnyAsync(ct => ct.MaHd == id);
            if (coChiTiet)
            {
                TempData["Error"] = "Không thể xóa hóa đơn vì đang có chi tiết hóa đơn liên kết.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.HoaDons.Remove(hoaDon);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa hóa đơn thành công!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi khi xóa: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }
    }
}





