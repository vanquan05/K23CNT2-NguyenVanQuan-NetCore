using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvqLesson8.Models;

namespace NvqLesson08Annotation.Controllers
{
    public class NvqAccountController : Controller
    {
        private static List<NvqAccount> NvqListAccount = new List<NvqAccount>()
        {
            new NvqAccount
            {
                NvqId = 230022113,
                NvqFullName = "Nguyen Van Quan",
                NvqEmail = "Nguyenvanquan@gmail.com",
                NvqPhone = "0775345205",
                NvqAddress = "Lớp K23CNT2",
                NvqAvatar = "vanquanj.jpg",
                NvqBirthday = new DateTime(2005, 6, 13),
                NvqGender = "Nam",
                NvqPassword = "077534205",
                NvqFacebook = "https://facebook.com/deveduvn"
            },
            new NvqAccount
            {
                NvqId = 2,
                NvqFullName = "Trần Thị B",
                NvqEmail = "tranthib@example.com",
                NvqPhone = "0987654321",
                NvqAddress = "456 Đường B, Quận 3, TP.HCM",
                NvqAvatar = "avatar2.jpg",
                NvqBirthday = new DateTime(1992, 8, 15),
                NvqGender = "Nữ",
                NvqPassword = "password2",
                NvqFacebook = "https://facebook.com/tranthib"
            },
            new NvqAccount
            {
                NvqId = 3,
                NvqFullName = "Lê Văn C",
                NvqEmail = "levanc@example.com",
                NvqPhone = "0911222333",
                NvqAddress = "789 Đường C, Quận 5, TP.HCM",
                NvqAvatar = "avatar3.jpg",
                NvqBirthday = new DateTime(1988, 12, 1),
                NvqGender = "Nam",
                NvqPassword = "password3",
                NvqFacebook = "https://facebook.com/levanc"
            },
            new NvqAccount
            {
                NvqId = 4,
                NvqFullName = "Phạm Thị D",
                NvqEmail = "phamthid@example.com",
                NvqPhone = "0909876543",
                NvqAddress = "321 Đường D, Quận 7, TP.HCM",
                NvqAvatar = "avatar4.jpg",
                NvqBirthday = new DateTime(1995, 3, 10),
                NvqGender = "Nữ",
                NvqPassword = "password4",
                NvqFacebook = "https://facebook.com/phamthid"
            },
            new NvqAccount
            {
                NvqId = 5,
                NvqFullName = "Hoàng Văn E",
                NvqEmail = "hoangvane@example.com",
                NvqPhone = "0933444555",
                NvqAddress = "654 Đường E, Quận 10, TP.HCM",
                NvqAvatar = "avatar5.jpg",
                NvqBirthday = new DateTime(1991, 7, 22),
                NvqGender = "Nam",
                NvqPassword = "password5",
                NvqFacebook = "https://facebook.com/hoangvane"
            }
        };

        // GET: NvqAccountController
        public ActionResult NvqIndex()
        {
            return View(NvqListAccount);
        }

        // GET: NvqAccountController/Details/5
        public ActionResult Details(int id)
        {
            var account = NvqListAccount.FirstOrDefault(x => x.NvqId == id);
            if (account == null) return NotFound();
            return View(account);
        }

        // GET: NvqAccountController/Create
        public ActionResult NvqCreate()
        {
            var NvqModel = new NvqAccount();
            return View(NvqModel);
        }

        // POST: NvqAccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NvqAccount NvqModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NvqListAccount.Add(NvqModel);
                    return RedirectToAction(nameof(NvqIndex));
                }
                return View(NvqModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm mới: " + ex.Message);
                return View(NvqModel);
            }
        }

        // GET: NvqAccountController/Edit/5
        public ActionResult Edit(int id)
        {
            var account = NvqListAccount.FirstOrDefault(x => x.NvqId == id);
            if (account == null) return NotFound();
            return View(account);
        }

        // POST: NvqAccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NvqAccount model)
        {
            try
            {
                var account = NvqListAccount.FirstOrDefault(x => x.NvqId == id);
                if (account == null) return NotFound();

                // Cập nhật dữ liệu
                account.NvqFullName = model.NvqFullName;
                account.NvqEmail = model.NvqEmail;
                account.NvqPhone = model.NvqPhone;
                account.NvqAddress = model.NvqAddress;
                account.NvqAvatar = model.NvqAvatar;
                account.NvqBirthday = model.NvqBirthday;
                account.NvqGender = model.NvqGender;
                account.NvqPassword = model.NvqPassword;
                account.NvqFacebook = model.NvqFacebook;

                return RedirectToAction(nameof(NvqIndex));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                return View(model);
            }
        }

        // GET: NvqAccountController/Delete/5
        public ActionResult Delete(int id)
        {
            var account = NvqListAccount.FirstOrDefault(x => x.NvqId == id);
            if (account == null) return NotFound();
            return View(account);
        }

        // POST: NvqAccountController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var account = NvqListAccount.FirstOrDefault(x => x.NvqId == id);
                if (account != null)
                {
                    NvqListAccount.Remove(account);
                }
                return RedirectToAction(nameof(NvqIndex));
            }
            catch
            {
                return View();
            }
        }
    }
}