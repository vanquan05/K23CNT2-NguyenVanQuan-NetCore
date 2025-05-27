using Microsoft.AspNetCore.Mvc;
using NvqLab6.Models;
using NvqLab6.Models;

namespace NvqLab6.Controllers
{
    public class NvqEmployeeController : Controller
    {
        public static List<NvqEmployee> NvqListEmployee = new List<NvqEmployee>
        {
        new NvqEmployee { NvqId = 1, NvqName = "Nguyen Van Quan", NvqBirthDay = new DateTime(2005, 6, 13), NvqEmail = "vanquan@example.com", NvqPhone = "0123456789", NvqSalary = 1000, NvqStatus = true },
        new NvqEmployee { NvqId = 2, NvqName = "Trần Thị B", NvqBirthDay = new DateTime(1998, 3, 10), NvqEmail = "thib@example.com", NvqPhone = "0987654321", NvqSalary = 1200, NvqStatus = true },
        new NvqEmployee { NvqId = 3, NvqName = "Lê Văn C", NvqBirthDay = new DateTime(2000, 8, 20), NvqEmail = "vanc@example.com", NvqPhone = "0909090909", NvqSalary = 900, NvqStatus = false },
        new NvqEmployee { NvqId = 4, NvqName = "Phạm Thị D", NvqBirthDay = new DateTime(1995, 1, 30), NvqEmail = "thid@example.com", NvqPhone = "0888888888", NvqSalary = 1100, NvqStatus = true },
        new NvqEmployee { NvqId = 5, NvqName = "Bạn Sinh Viên", NvqBirthDay = new DateTime(2002, 10, 1), NvqEmail = "sinhvien@example.com", NvqPhone = "0777777777", NvqSalary = 950, NvqStatus = true },
    };

        public IActionResult NvqIndex()
        {
            return View(NvqListEmployee);
        }
        public IActionResult NvqCreate()
        {

            return View();
        }

        public IActionResult NvqCreateSubmit(NvqEmployee NvqEmp)
        {
            NvqEmp.NvqId = NvqListEmployee.Max(e => e.NvqId) + 1;
            NvqListEmployee.Add(NvqEmp);
            return RedirectToAction("NvqIndex");
        }

        [HttpGet]
        public IActionResult NvqEdit(int id)
        {
            var NvqEmp = NvqListEmployee.FirstOrDefault(e => e.NvqId == id);
            if (NvqEmp == null)
                return NotFound();
            return View(NvqEmp);
        }
        public IActionResult NvqEditPUT(NvqEmployee NvqEmp)
        {
            var existing = NvqListEmployee.FirstOrDefault(e => e.NvqId == NvqEmp.NvqId);
            if (existing != null)
            {
                existing.NvqName = NvqEmp.NvqName;
                existing.NvqBirthDay = NvqEmp.NvqBirthDay;
                existing.NvqEmail = NvqEmp.NvqEmail;
                existing.NvqPhone = NvqEmp.NvqPhone;
                existing.NvqSalary = NvqEmp.NvqSalary;
                existing.NvqStatus = NvqEmp.NvqStatus;
            }
            return RedirectToAction("NvqIndex");
        }

        public IActionResult NvqDelete(int id)
        {
            var NvqEmp = NvqListEmployee.FirstOrDefault(e => e.NvqId == id);
            if (NvqEmp != null)
                NvqListEmployee.Remove(NvqEmp);
            return RedirectToAction("NvqIndex");
        }
    }
}

