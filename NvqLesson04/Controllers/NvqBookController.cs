using Microsoft.AspNetCore.Mvc;
using NvqLesson04.Models;

namespace NvqLesson04.Controllers
{
    public class NvqBookController : Controller
    {

        private List<NvqBook> nvqBooks = new List<NvqBook>
{
    new NvqBook
    {
        NvqId = "B001",
        NvqTitle = "Lập Trình C# Cơ Bản",
        NvqDescription = "Giới thiệu về lập trình C# cho người mới bắt đầu.",
        NvqImage = "book-3.jpg",
        NvqPrice = "120000",
        NvqPage = "300"
    },
    new NvqBook
    {
        NvqId = "B002",
        NvqTitle = "ASP.NET Core Nâng Cao",
        NvqDescription = "Hướng dẫn chuyên sâu về ASP.NET Core và MVC.",
        NvqImage = "book-12.jpg",
        NvqPrice = "150000",
        NvqPage = "400"
    },
    new NvqBook
    {
        NvqId = "B003",
        NvqTitle = "Cấu Trúc Dữ Liệu & Giải Thuật",
        NvqDescription = "Học cách tổ chức và xử lý dữ liệu hiệu quả.",
        NvqImage = "book-27.jpg",
        NvqPrice = "130000",
        NvqPage = "350"
    },
    new NvqBook
    {
        NvqId = "B004",
        NvqTitle = "SQL Cho Người Mới Bắt Đầu",
        NvqDescription = "Tìm hiểu ngôn ngữ truy vấn SQL một cách dễ hiểu.",
        NvqImage = "book-40.jpg",
        NvqPrice = "100000",
        NvqPage = "280"
    },
    new NvqBook
    {
        NvqId = "B005",
        NvqTitle = "Thiết Kế Giao Diện Với HTML & CSS",
        NvqDescription = "Hướng dẫn thiết kế giao diện web chuẩn HTML/CSS.",
        NvqImage = "book-51.jpg",
        NvqPrice = "110000",
        NvqPage = "320"
    }

        // GET: /NvqBook/NvqIndex -> Lay tat ca cac cuon sach 
        public IActionResult NvqIndex()
        {

            // Đưa dữ liệu trên view

            return View(nvqBooks);
        }
    }
}
