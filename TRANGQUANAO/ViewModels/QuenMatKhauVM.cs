using System.ComponentModel.DataAnnotations;

namespace TRANGQUANAO.ViewModels
{
    public class QuenMatKhauVM
    {
        [Required(ErrorMessage = "Vui lòng nhập email đã đăng ký.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
    }
}
