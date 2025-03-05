using Microsoft.EntityFrameworkCore;
using GraphQLBooksAPI.Models;

namespace GraphQLBooksAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Authors
        modelBuilder.Entity<Author>().HasData(
            new Author { AuthorId = 1, Nickname = "มาริโอ้", Firstname = "มาริโอ้", Lastname = "เมาเร่อ", BirthdayDate = new DateTime(1988, 12, 4).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Mario_Maurer_in_May_2024.png/400px-Mario_Maurer_in_May_2024.png" },
            new Author { AuthorId = 2, Nickname = "ญาญ่า", Firstname = "อุรัสยา", Lastname = "เสปอร์บันด์", BirthdayDate = new DateTime(1993, 3, 18).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/aa/Urassaya_Sperbund_in_October_2022.png" },
            new Author { AuthorId = 3, Nickname = "อั้ม", Firstname = "พัชราภา", Lastname = "ไชยเชื้อ", BirthdayDate = new DateTime(1978, 12, 5).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Patcharapa_Chaichua_in_2025.png/440px-Patcharapa_Chaichua_in_2025.png" },
            new Author { AuthorId = 4, Nickname = "มาร์ช", Firstname = "จุฑาวุฒิ", Lastname = "ภัทรกำพล", BirthdayDate = new DateTime(1993, 11, 18).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/96/Chutavuth_2015.jpg" },
            new Author { AuthorId = 5, Nickname = "เต้ย", Firstname = "จรินทร์พร", Lastname = "จุนเกียรติ", BirthdayDate = new DateTime(1989, 5, 26).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/45/JARINPORN_JOONKIAT_P2.jpg" },
            new Author { AuthorId = 6, Nickname = "พลอยชมพู", Firstname = "ญานนีน", Lastname = "ภารวี ไวเกล", BirthdayDate = new DateTime(1994, 4, 8).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Jannine_weigel_2018-9-21.jpg/440px-Jannine_weigel_2018-9-21.jpg" },
            new Author { AuthorId = 7, Nickname = "ออม", Firstname = "กรณ์นภัส", Lastname = "เศรษฐรัตนพงศ์", BirthdayDate = new DateTime(1989, 4, 17).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0a/Orm_Kornnaphat_%40_The_Secret_Of_Us.png" },
            new Author { AuthorId = 8, Nickname = "กลัฟ", Firstname = "คณาวุฒิ", Lastname = "ไตรพิพัฒนพงษ์", BirthdayDate = new DateTime(1989, 10, 21).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0a/%E0%B8%84%E0%B8%93%E0%B8%B2%E0%B8%A7%E0%B8%B8%E0%B8%92%E0%B8%B4_%E0%B8%81%E0%B8%A5%E0%B8%B1%E0%B8%9F_Project.jpg" },
            new Author { AuthorId = 9, Nickname = "มาสุ", Firstname = "มาสุ", Lastname = "จรรยางค์ดีกุล", BirthdayDate = new DateTime(1990, 5, 11).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/34/%E0%B8%A1%E0%B8%B2%E0%B8%AA%E0%B8%B8_%E0%B8%88%E0%B8%A3%E0%B8%A3%E0%B8%A2%E0%B8%B2%E0%B8%87%E0%B8%84%E0%B9%8C%E0%B8%94%E0%B8%B5%E0%B8%81%E0%B8%B8%E0%B8%A5_%28%E0%B8%A1%E0%B8%B2%E0%B8%AA%E0%B8%B8%29_Happy_New_Year_2023.png" },
            new Author { AuthorId = 10, Nickname = "ดีเจพุฒ", Firstname = "พุฒิชัย", Lastname = "เกษตรสิน", BirthdayDate = new DateTime(1984, 6, 21).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Puttichai_Kasetsin_in_2016_Sept.jpg" },
            new Author { AuthorId = 11, Nickname = "ขวัญ", Firstname = "อุษามณี", Lastname = "ไวทยานนท์", BirthdayDate = new DateTime(1992, 6, 15).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/Kwan_Usamanee_Waitayanon.jpg/440px-Kwan_Usamanee_Waitayanon.jpg" },
            new Author { AuthorId = 12, Nickname = "คิม", Firstname = "คิมเบอร์ลี่", Lastname = "แอน โวลเทมัส", BirthdayDate = new DateTime(1991, 10, 28).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/11/%E0%B8%84%E0%B8%B4%E0%B8%A1%E0%B9%80%E0%B8%9A%E0%B8%AD%E0%B8%A3%E0%B9%8C%E0%B8%A5%E0%B8%B5%E0%B9%88_%E0%B9%81%E0%B8%AD%E0%B8%99_%E0%B9%82%E0%B8%A7%E0%B8%A5%E0%B9%80%E0%B8%97%E0%B8%A1%E0%B8%B1%E0%B8%AA_%28%E0%B8%84%E0%B8%B4%E0%B8%A1%E0%B8%A1%E0%B8%B5%E0%B9%88%29_Happy_New_Year_2023.png" },
            new Author { AuthorId = 13, Nickname = "บี้", Firstname = "สุกฤษฎิ์", Lastname = "วิเศษแก้ว", BirthdayDate = new DateTime(1989, 7, 5).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3d/Sukrit_Wisetkaew.jpg/440px-Sukrit_Wisetkaew.jpg" },
            new Author { AuthorId = 14, Nickname = "ติ๊ต๊ะ", Firstname = "ชญานิศ", Lastname = "จ่ายเจริญ", BirthdayDate = new DateTime(1995, 9, 15).ToString(), CoverProfileImageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Chayanit_Chayjaroen_%40TOMFORDeyewear%2C_2024.jpg" },
            new Author { AuthorId = 15, Nickname = "ไผ่", Firstname = "ไผ่", Lastname = "พงศธร", BirthdayDate = new DateTime(1993, 12, 24).ToString(), CoverProfileImageUrl = "https://m.thaiticketmajor.com/img_artist/prefix_1/0353/353/353-600e74fbc4648-a.jpg" }
        );




        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 1, BookName = "The Let Them Theory", AuthorId = 1, Price = 20.99m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/91ZVf3kNrcL._SL1500_.jpg" },
            new Book { BookId = 2, BookName = "How to Stop Overthinking", AuthorId = 2, Price = 25.50m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/61e70IONyML._SL1500_.jpg" },
            new Book { BookId = 3, BookName = "Stop Overthinking", AuthorId = 3, Price = 30.00m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/41JdmVc+mHL._SY445_SX342_.jpg" },
            new Book { BookId = 4, BookName = "LIVE WITHOUT ANXIETY", AuthorId = 4, Price = 29.00m, Rating = 4, CoverBookImageUrl = "https://m.media-amazon.com/images/I/41BHt2MGouL._SY445_SX342_.jpg" },
            new Book { BookId = 5, BookName = "Let's Stop Overthinking", AuthorId = 5, Price = 27.00m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/61JqJte2F2L._SY522_.jpg" },
            new Book { BookId = 6, BookName = "How To Stop Overthinking Everything", AuthorId = 6, Price = 25.00m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/717REeZ27-L._SY522_.jpg" },
            new Book { BookId = 7, BookName = "Solution to Stop Overthinking Your Relationship", AuthorId = 7, Price = 18.00m, Rating = 4, CoverBookImageUrl = "https://m.media-amazon.com/images/I/61ImI9wz84L._SY522_.jpg" },
            new Book { BookId = 8, BookName = "Mindset Mastery", AuthorId = 8, Price = 20.00m, Rating = 3, CoverBookImageUrl = "https://m.media-amazon.com/images/I/91dwIJk-iuL._SY522_.jpg" },
            new Book { BookId = 9, BookName = "No days off", AuthorId = 9, Price = 20.55m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/61j0rbnTMnL._SY522_.jpg" },
            new Book { BookId = 10, BookName = "50 Things to Know About Choosing the Right Path in Life", AuthorId = 10, Price = 16.99m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/51sPZW1FolL._SY445_SX342_.jpg" },
            new Book { BookId = 11, BookName = "The Most Powerful Goal Achievement System in the World ", AuthorId = 11, Price = 20.99m, Rating = 3, CoverBookImageUrl = "https://m.media-amazon.com/images/I/81NRzCBfb0L._SY522_.jpg" },
            new Book { BookId = 12, BookName = "Real Change", AuthorId = 12, Price = 44.00m, Rating = 2, CoverBookImageUrl = "https://m.media-amazon.com/images/I/81rgnK3S-7L._SY522_.jpg" },
            new Book { BookId = 13, BookName = "Without HOPE there is no FUTURE", AuthorId = 13, Price = 31.00m, Rating = 5, CoverBookImageUrl = "https://m.media-amazon.com/images/I/617cxF0kBVL._SY522_.jpg" },
            new Book { BookId = 14, BookName = "Quick Sand", AuthorId = 14, Price = 43.00m, Rating = 4, CoverBookImageUrl = "https://m.media-amazon.com/images/I/71JhwhR0k-L._SY522_.jpg" },
            new Book { BookId = 15, BookName = "Clear Space, Clear Mind", AuthorId = 15, Price = 55.00m, Rating = 2, CoverBookImageUrl = "https://m.media-amazon.com/images/I/412946hqcVL._SY445_SX342_.jpg" }
        );
    }
}