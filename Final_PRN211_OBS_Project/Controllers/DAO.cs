using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class DAO
    {
        Model db = new Model();
        //Book DAO
        #region
        public List<Book> GetNewestBook()
        {
            return db.Books.SqlQuery("SELECT TOP 7 * FROM [OnlineBookShop].[dbo].[Book] where [status] = 1 ORDER BY id DESC").ToList();
        }

        public List<Book> GetBestSellerBook()
        {
            String sql = "select id,title,image_url,author_id,[description],price,[status] from "
                  + "(SELECT top 7 b.id, b.title, b.[image_url], b.author_id, b.[description], b.price, b.[status], Sum(quantity) as Qty"
                  + " from Orderline o inner join Book b on (o.book_id = b.id) where b.[status] = 1"
                  + " group by b.id, b.title, b.[image_url], b.author_id, b.[description], b.price, b.[status]"
                  + " order by Qty desc, b.price desc) x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public Book GetBookById(string id)
        {
            return db.Books.SqlQuery($"SELECT * FROM Book WHERE id = '{id}'").First();
        }

        public List<Book> GetRelatedBook(Book b)
        {
            List<Book> list = db.Books.SqlQuery($"SELECT * From Book where author_id = {b.author_id} and id != {b.id} and [status] = 1 ").ToList();
            string sql = "select id,title,image_url,author_id,[description],price,[status] from"
                       + " (select * from Book_Genre bg inner join Book b on (bg.book_id = b.id)"
                       + $" where genre_id = {GetGenreByBookId(b.id.ToString()).First().id}"
                       + $" and id != {b.id} and [status] = 1) x";
            List<Book> relateByGenre = db.Books.SqlQuery(sql).ToList();
            foreach (var item in relateByGenre)
            {
                list.Add(item);
            }
            if (list.Count > 4) return list.GetRange(0, 4);
            return list;
        }

        public List<Book> GetBookByGenre(string id)
        {
            string sql = "select id,title,image_url,author_id,[description],price,[status] from "
                       + $"(select * from Book_Genre bg inner join Book b on (bg.book_id = b.id) where genre_id = '{id}' and [status] = 1) x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public List<Book> getBookByFilter(string[] arr)
        {
            if (arr == null)
            {
                return db.Books.SqlQuery("select * from Book where [status] = 1").ToList();
            }
            string sql = "select b.id, b.title, b.image_url, b.author_id, b.[description], b.price, b.[status],count(b.id) from (Book b inner join Book_Genre bg on (b.id = bg.book_id)) where ((";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1) sql += $"genre_id = {arr[i]}) and b.[status] = 1) ";
                else sql += $"genre_id = {arr[i]} or ";
            }
            sql += $"group by b.id, b.title, b.image_url, b.author_id, b.[description],b.price, b.[status] having COUNT(b.id)={arr.Length}";
            return db.Books.SqlQuery(sql).ToList().Distinct().ToList();
        }

        public List<Book> GetBooksByAuthor(int auid)
        {
            return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where author_id = {auid} and [status] = 1").Distinct().ToList();
        }

        public List<Book> GetBooksByName(string name)
        {
            try
            {
                if (!name.Equals(""))
                    return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where title like '%{name}%' and [status] = 1").ToList();
                else
                {
                    return db.Books.SqlQuery("select * from Book where [status] = 1").ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Book> GetBooks()
        {
            return db.Books.SqlQuery("Select * from Book").ToList();
        }

        public void DeleteBookById(int book_id)
        {
            db.Database.ExecuteSqlCommand($"delete from Book_Genre where book_id = '{book_id}'");
            db.Database.ExecuteSqlCommand($"delete from Review where book_id = '{book_id}'");
            db.Database.ExecuteSqlCommand($"delete from Stock where book_id = '{book_id}'");
            db.Database.ExecuteSqlCommand($"delete from Book where id = '{book_id}'");
            db.SaveChanges();
        }

        public void ChangeStatus(int book_id, int status)
        {
            db.Database.ExecuteSqlCommand($"update Book set [status] = {status} where id = '{book_id}'");
            db.SaveChanges();
        }
        #endregion

        //Genre DAO
        #region
        public List<Genre> GetGenres()
        {
            return db.Genres.SqlQuery("SELECT * FROM [OnlineBookShop].[dbo].[Genre]").ToList();
        }

        public List<Genre> GetGenreByBookId(string id)
        {
            string sql = $"select x.id, x.name from(select * from Book_Genre bg inner join Genre g on(bg.genre_id=g.id) where book_id = {id}) x";
            return db.Genres.SqlQuery(sql).ToList();
        }

        public Genre GetGenreById(string id)
        {
            return db.Genres.SqlQuery($"select * from Genre where id = '{id}'").First();
        }

        public int InsertGenreBook(int bookId, int genreId)
        {
            return db.Database.ExecuteSqlCommand($"insert into Book_Genre " +
                $"(book_id, genre_id ) values ({bookId},{genreId})");
        }

        public int DeleteBookGenreId(int BookId)
        {
            return db.Database.ExecuteSqlCommand($"delete from Book_Genre where book_id = {BookId}");
        }

        public bool IsInList(List<Genre> g, int id)
        {
            foreach (var x in g)
            {
                if (x.id == id) return true;
            }
            return false;
        }

        public void DeleteGenre(string id)
        {
            Genre x = GetGenreById(id);
            db.Genres.Remove(x);
            db.SaveChanges();
        }

        public void AddGenre(string title)
        {
            db.Database.ExecuteSqlCommand($"insert into Genre([name]) values ('{title}')");
            db.SaveChanges();
        }

        public void EditGenre(string id, string name)
        {
            db.Database.ExecuteSqlCommand($"update Genre set [name] = '{name}' where [id] = '{id}'");
            db.SaveChanges();
        }
        #endregion

        //Author DAO
        #region
        public Author GetAuthorById(string id)
        {
            return db.Authors.SqlQuery($"SELECT * FROM Author WHERE id = {id}").First();
        }

        public List<Author> GetAllAuthors()
        {
            return db.Authors.ToList();
        }
        #endregion

        //User DAO 
        #region
        public List<User> GetUsers()
        {
            try
            {
                return db.Users.SqlQuery("select * from [User]").ToList();
            }
            catch
            {
                return null;
            }
        }

        public User GetUserLogin(string email, string pass)
        {
            try
            {
                User x = db.Users.SqlQuery($"select * from [User] where BINARY_CHECKSUM(email) = BINARY_CHECKSUM('{email}') and BINARY_CHECKSUM([password]) = BINARY_CHECKSUM('{pass}')").First();
                return x;
            }
            catch
            {
                return null;
            }
        }

        public User GetUserByEmail(string email)//email ton tai => true, email k ton tai => sai
        {
            try
            {
                User x = db.Users.SqlQuery($"SELECT * FROM [User] WHERE BINARY_CHECKSUM(email) = BINARY_CHECKSUM('{email}')").First();
                return x;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void RegistUser(string email, string password, string name)
        {
            db.Users.Add(new User() { email = email, password = password, username = name, avatar_url = "https://avatars.githubusercontent.com/u/17879520?v=4", role_id = 2 });
            db.SaveChanges();
        }

        public User GetUserByID(string id)
        {
            try
            {
                return db.Users.SqlQuery($"Select * from [User] where id = '{id}'").First();
            }
            catch
            {
                return null;
            }
        }

        public void ChangeStatusUser(int id, int status)
        {
            var change = db.Users.SingleOrDefault(c => c.id == id);
            change.status = status;
            db.SaveChanges();
        }

        public bool CheckExist(string name)
        {
            User x = new User(); 
            try
            {
                x = db.Users.SqlQuery($"select * from [User] where email = '{name}'").First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateUser(string email, string name, string id)
        {
            db.Database.ExecuteSqlCommand($"UPDATE [User] SET [email] = '{email}', [username] = '{name}' WHERE id = {id}; ");
            db.SaveChanges();

        }

        public void UpdatePass(string pass, string email)
        {
            db.Database.ExecuteSqlCommand($"UPDATE [User] SET  [password] = '{pass}' WHERE [email] = '{email}'; ");
            db.SaveChanges();
        }
        #endregion

        //Orderline DAO
        #region
        public Orderline GetOrderlineByBookId(List<Orderline> order, int id)
        {
            if (order == null) return null;
            foreach (var item in order)
            {
                if (item.book_id == id) return item;
            }
            return null;
        }

        public List<Orderline> AddToCart(List<Orderline> list, int book_id, int quantity)
        {
            Orderline orderline = GetOrderlineByBookId(list, book_id);
            if (orderline == null)
            {
                list.Add(new Orderline { book_id = book_id, quantity = quantity });
                return list;
            }
            orderline.quantity = quantity;
            return list;
        }

        public List<Orderline> RemoveFromCart(List<Orderline> list, int book_id)
        {
            list.Remove(GetOrderlineByBookId(list, book_id));
            return list;
        }

        public void AddOrderline(Orderline orderline)
        {
            db.Orderlines.Add(new Orderline { bill_id = IDOfNewBill(), book_id = orderline.book_id, quantity = orderline.quantity });
            db.SaveChanges();
        }

        public List<Orderline> GetOrderlineByBookId(int id)
        {
            return db.Orderlines.SqlQuery($"select * from Orderline where book_id = '{id}'").ToList();
        }

        public List<Orderline> GetOrderlinesInBill(string id)
        {
            try
            {
                return db.Orderlines.SqlQuery($"Select * from Orderline where bill_id = {id}").ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        //Bill DAO
        #region
        public List<Bill> GetBills()
        {
            return db.Bills.SqlQuery("SELECT * FROM Bill where [status] <> 'Unseen' order by [date] desc").ToList();
        }

        public void AddBill(Bill bill)
        {
            db.Bills.Add(new Bill { user_id = bill.user_id, address = bill.address, telephone = bill.telephone, date = DateTime.Now, payment = bill.payment, status = "Unseen", total = bill.total });
            db.SaveChanges();
        }

        public int IDOfNewBill()
        {
            try
            {
                return db.Bills.SqlQuery("select * from Bill order by id desc").First().id;
            }
            catch
            {
                return 1;
            }
        }

        public List<Bill> GetUnseenBill()
        {
            string sql = "select * from Bill where status = 'Unseen'";
            return db.Bills.SqlQuery(sql).ToList();
        }

        public List<Bill> GetUserBillHistoryByUserID(string id)
        {
            try
            {
                return db.Bills.SqlQuery($"select * from Bill where [user_id] = '{id}'").ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Bill> GetAllBill()
        {
            string sql = "select * from Bill";
            return db.Bills.SqlQuery(sql).ToList();
        }
        public void ChangeStatus(int id, string status)
        {
            var change = db.Bills.SingleOrDefault(c => c.id == id);
            change.status = status;
            db.SaveChanges();
        }
        public List<Orderline> GetBillDeltai(int id)
        {

            return db.Orderlines.SqlQuery($"select *from OrderLine where bill_id ={id}").ToList();
        }

        public double GetRevenueInMonthYear(int month, int year)
        {
            double total = 0;
            List<Bill> bills = db.Bills.SqlQuery($"SELECT * FROM Bill where month([date]) = {month} and YEAR([date]) = {year} and [status] = 'Done' ").ToList();
            foreach (var item in bills)
            {
                total += item.total;
            }
            if (month <= DateTime.Now.Month) return total;
            return -1;

        }
        #endregion

        //StockDAO
        #region
        public Stock GetStockByBookId(int id)
        {
            return db.Stocks.SqlQuery($"select * from Stock where book_id = '{id}'").First();
        }
        #endregion

        //RoleDAO
        #region
        public Role GetRole(int id)
        {
            return db.Roles.SqlQuery($"select * from Role where id = {id}").First();
        }
        #endregion

        //ReviewDAO
        #region
        public List<Review> GetReviewOfBookID(string id)
        {
            return db.Reviews.SqlQuery($"select * from Review where book_id = '{id}'").ToList();
        }

        public void AddReview(int book_id, int user_id, string comment)
        {
            Review x = new Review { book_id = book_id, user_id = user_id, comment = comment, date = DateTime.Now };
            db.Reviews.Add(x);
            db.SaveChanges();
        }
        #endregion

        //Access Request
        #region
        public bool isAble(User x, string role)
        {
            if (x == null) return false;
            return GetRole(x.role_id).title.Equals(role);
        }
        #endregion

        //Đang code
        #region

        public List<Book> GetLowStockBook()
        {
            return db.Books.SqlQuery("select  * from Book b inner join Stock s on (b.id = s.book_id) where s.quantity < 10 order by quantity asc").ToList();
        }

        public void UpdateStock(string id, int quantity)
        {
            Stock stock = GetStockByBookId(Convert.ToInt32(id));
            db.Database.ExecuteSqlCommand($"update Stock set [quantity] = {quantity+stock.quantity} where [book_id] = '{id}' ");
            db.SaveChanges();
        }
        #endregion
    }
}