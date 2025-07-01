using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NvqLesson8.Models
{
    public class NvqAccount
    {
        [Key]
        [Display(Name = "Mã")]
        public int NvqId { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [MinLength(6, ErrorMessage = "Họ tên ít nhất 6 ký tự")]
        [MaxLength(20, ErrorMessage = "Họ tên tối đa 20 ký tự")]
        public string NvqFullName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
        public string NvqEmail { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+?(?:[0-9]{3}))?[ .-]?(?:[0-9]{3})?[ .-]?(?:[0-9]{4}){1,2}$",
            ErrorMessage = "Số điện thoại không đúng định dạng")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string NvqPhone { get; set; }

        [Display(Name = "Địa chỉ thường trú")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(35, ErrorMessage = "Địa chỉ không vượt quá 35 ký tự")]
        public string NvqAddress { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string NvqAvatar { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        public DateTime NvqBirthday { get; set; }

        [Display(Name = "Giới tính")]
        public string NvqGender { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
        public string NvqPassword { get; set; }

        [Display(Name = "Facebook")]
        public string NvqFacebook { get; set; }
       {
    private readonly YourDbContext _context;

        public NvqAccountController(YourDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var account = _context.NvqAccounts.Find(id);
            if (account != null)
            {
                _context.NvqAccounts.Remove(account);
                _context.SaveChanges();
            }
            return RedirectToAction("NvqIndex");
        }
    }
}
