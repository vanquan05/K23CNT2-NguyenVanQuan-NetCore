namespace TRANGQUANAO.ViewModels
{
    public class FeedbackViewModel
    {
        public string TenKhachHang { get; set; }
        public string MucDoHaiLong { get; set; } // Rất hài lòng, Hài lòng, Không hài lòng
        public string LyDoKhongHaiLong { get; set; }
        public int SoSao { get; set; }  // 1 - 5 Sao
    }
}
