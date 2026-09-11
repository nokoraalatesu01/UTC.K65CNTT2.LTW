using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace SachMVCLab03.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Sumary { get; set; }

        public List<Book> GetBooksList()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Slam Dunk",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b1.png",
                    Price = 30000,
                    Sumary = "",
                    TotalPage = 50
                },
                new Book()
                {
                    Id = 2,
                    Title = "Conan",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b2.png",
                    Price = 25000,
                    Sumary = "",
                    TotalPage = 50
                },
                new Book()
                {
                    Id = 3,
                    Title = "One Piece",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b3.png",
                    Price = 30000,
                    Sumary = "",
                    TotalPage = 50
                },
                new Book()
                {
                    Id = 4,
                    Title = "Wing books",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b4.png",
                    Price = 300000,
                    Sumary = "",
                    TotalPage = 500
                },
            };
            return books;
        }

        public Book GetBookByID(int id)
        {
            Book book = this.GetBooksList().FirstOrDefault(b => b.Id == id);
            return book;
        }

        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Takehiko Inoue"},
            new SelectListItem {Value="2", Text = "Gosho Aoyama"},
            new SelectListItem {Value="3", Text = "Atchaikenai Yatsu"},
            new SelectListItem {Value="4", Text = "Gin Shirakawa"},
        };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "1", Text = "Truyện Tranh"},
            new SelectListItem {Value = "2", Text = "Trinh thám"},
            new SelectListItem {Value = "3", Text = "Siêu nhiên"},
            new SelectListItem {Value = "4", Text = "Tiểu thuyết"},
        };
    }
}
