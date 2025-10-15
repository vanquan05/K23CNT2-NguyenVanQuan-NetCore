using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRANGQUANAO.Models;

namespace TRANGQUANAO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangHoasController : AdminBaseController
    {
        private readonly TrangquanaoContext _context;

        public HangHoasController(TrangquanaoContext context)
        {
            _context = context;
        }

        // GET: Admin/HangHoas
        public async Task<IActionResult> Index()
        {
            var trangquanaoContext = _context.HangHoas.Include(h => h.MaLoaiNavigation).Include(h => h.MaNccNavigation);
            return View(await trangquanaoContext.ToListAsync());
        }

        // GET: Admin/HangHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: Admin/HangHoas/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: Admin/HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHh,TenHh,TenAlias,MaLoai,MoTaDonVi,DonGia,Hinh,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(hangHoa);
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
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);

            return View(hangHoa);
        }

        // GET: Admin/HangHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
                return NotFound();

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // POST: Admin/HangHoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,TenAlias,MaLoai,MoTaDonVi,DonGia,Hinh,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa)
        {
            if (id != hangHoa.MaHh)
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

                ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
                ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
                return View(hangHoa);
            }

            try
            {
                _context.Update(hangHoa);
                await _context.SaveChangesAsync();
                Console.WriteLine("==== ĐÃ LƯU THÀNH CÔNG ====");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("==== LỖI KHI LƯU ====");
                Console.WriteLine(ex.Message);
            }

            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: Admin/HangHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: Admin/HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas
        .Include(h => h.ChiTietHds)
        .FirstOrDefaultAsync(h => h.MaHh == id);

            if (hangHoa == null)
            {
                return NotFound();
            }

            if (hangHoa.ChiTietHds.Any())
            {
                TempData["Error"] = "Không thể xóa sản phẩm này vì đang có trong hóa đơn!";
                return RedirectToAction(nameof(Index));
            }

            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHh == id);
        }
    }
}
