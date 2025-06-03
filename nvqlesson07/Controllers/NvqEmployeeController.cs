using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using nvqlesson07.Models;

namespace nvqlesson07.Controllers
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
        // GET: NvqEmployeeController
        public ActionResult NvqIndex()
        {
            return View(NvqListEmployee);
        }

        // GET: NvqEmployeeController/Details/5
        public ActionResult NvqDetails(int id)
        {
            var nvqEmployee = NvqListEmployee.FirstOrDefault(e => e.NvqId == id);
            return View(nvqEmployee);
        }

        // GET: NvqEmployeeController/Create
        public ActionResult NvqCreate()
        {
            var nvqEmplyee = new NvqEmployee();
            return View();
        }

        // POST: NvqEmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NvqCreate(NvqEmployee nvqModel)
        {

            try
            {
                nvqModel.NvqId = NvqListEmployee.Max(e => e.NvqId) + 1;
                NvqListEmployee.Add(nvqModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NvqEmployeeController/Edit/5
        public ActionResult NvqEdit(int id)
        {
            var nvqEmployee = NvqListEmployee.FirstOrDefault(e => e.NvqId == id);
            return View(nvqEmployee);
        }

        // POST: NvqEmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NvqEdit(int id, NvqEmployee nvqModel)
        {
            try
            {
                for (int i = 0; i < NvqListEmployee.Count(); i++) 
                {
                    if (NvqListEmployee[i].NvqId == id)
                    {
                        NvqListEmployee[i] = nvqModel;
                       break;
                    }
                }

                return RedirectToAction(nameof(NvqIndex));
            }
            catch
            {
                return View();
            }
        }

        // GET: NvqEmployeeController/Delete/5
        public ActionResult NvqDelete(int id)
        {
            
            return View();
        }

        // POST: NvqEmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
