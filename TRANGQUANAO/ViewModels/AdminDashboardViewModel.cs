using TRANGQUANAO.Models;

namespace TRANGQUANAO.ViewModels
{
    public class AdminDashboardViewModel
        {
            public int TotalProducts { get; set; }
            public int TotalOrders { get; set; }
            public int TotalCustomers { get; set; }
            public int TotalSuppliers { get; set; }
            public int TotalEmployees { get; set; }

            // 1 số mục mẫu để hiển thị recent items
            public List<HangHoa> RecentProducts { get; set; } = new List<HangHoa>();
        }
}
