using System.Net.Mail;
using System.Net;

namespace TRANGQUANAO.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            // Cấu hình tài khoản gửi (bạn chỉnh theo email thật)
            var fromAddress = new MailAddress("vulinhsatthu123@gmail.com", "Hệ thống quần áo");
            var toAddress = new MailAddress(to);
            const string fromPassword = "app_password_của_bạn"; // dùng App Password Gmail

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
