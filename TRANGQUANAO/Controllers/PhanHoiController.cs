using Microsoft.AspNetCore.Mvc;
using TRANGQUANAO.ViewModels;

namespace TRANGQUANAO.Controllers
{
    public class PhanHoiController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FeedbackViewModel());
        }

        [HttpPost]
        public IActionResult GuiPhanHoi(FeedbackViewModel model)
        {
            if (model.MucDoHaiLong == "Không hài lòng" && string.IsNullOrEmpty(model.LyDoKhongHaiLong))
            {
                ModelState.AddModelError("LyDoKhongHaiLong", "Vui lòng ghi lý do nếu không hài lòng.");
                return View("Index", model);
            }

            if (model.SoSao <= 2 && string.IsNullOrEmpty(model.LyDoKhongHaiLong))
            {
                ModelState.AddModelError("LyDoKhongHaiLong", "Vui lòng ghi lý do nếu đánh giá thấp.");
                return View("Index", model);
            }
            TempData["Message"] = "Cảm ơn bạn đã gửi phản hồi!";
            return RedirectToAction("Index");
        }
    }
}
