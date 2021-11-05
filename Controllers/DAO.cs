using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    public class DAO
    {
        Model db = new Model();
        //Book DAO
        #region
        public List<Book> GetNewestBook()
        {
            return db.Books.SqlQuery("SELECT TOP 7 * FROM [OnlineBookShop].[dbo].[Book] ORDER BY id DESC").ToList();
        }

        public List<Book> GetBestSellerBook()
        {
            String sql = "select id,title,image_url,author_id,[description],price from"
                  + "(SELECT top 7 b.id, b.title, b.[image_url], b.author_id, b.[description], b.price, Sum(quantity) as Qty"
                  + " from Orderline o inner join Book b on (o.book_id = b.id)"
                  + " group by b.id, b.title, b.[image_url], b.author_id, b.[description], b.price"
                  + " order by Qty desc, b.price desc) x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public Book GetBookById(string id)
        {
            return db.Books.SqlQuery($"SELECT * FROM Book WHERE id = {id}").First();
        }

        public List<Book> GetRelatedBook(Book b)
        {
            List<Book> list = db.Books.SqlQuery($"SELECT * From Book where author_id = {b.author_id} and id != {b.id}").ToList();
            string sql = "select id,title,image_url,author_id,[description],price from"
                       + " (select * from Book_Genre bg inner join Book b on (bg.book_id = b.id)"
                       + $" where genre_id = {GetGenreByBookId(b.id.ToString()).First().id}"
                       + $" and id != {b.id}) x";
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
            string sql = "select id,title,image_url,author_id,[description],price from "
                       + $"(select * from Book_Genre bg inner join Book b on (bg.book_id = b.id) where genre_id = '{id}') x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public List<Book> getBookByFilter(string[] arr)
        {
            if (arr == null)
            {
                return db.Books.SqlQuery("select * from Book").ToList();
            }
            string sql = "select b.id, b.title, b.image_url, b.author_id, b.[description], b.price from (Book b inner join Book_Genre bg on (b.id = bg.book_id)) where ";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1) sql += $"genre_id = {arr[i]} ";
                else sql += $"genre_id = {arr[i]} or ";
            }
            return db.Books.SqlQuery(sql).ToList().Distinct().ToList();
        }

        public List<Book> GetBooksByAuthor(int auid)
        {
            return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where author_id = {auid}").Distinct().ToList();
        }

        public List<Book> GetBooksByName(string name)
        {
            try
            {
                if (!name.Equals(""))
                    return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where title like '%{name}%'").ToList();
                else
                {
                    return db.Books.SqlQuery("select * from Book").ToList();
                }
            }
            catch
            {
                return null;
            }
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
        #endregion

        //Author DAO
        #region
        public Author GetAuthorById(string id)
        {
            return db.Authors.SqlQuery($"SELECT * FROM Author WHERE id = {id}").First();
        }
        #endregion

        //User DAO 
        #region
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

        public List<User> AllUser()
        {
            return db.Users.SqlQuery("select * From [User]").ToList();
        }

        public User GetUserByIdWithRecentBillInOneMonth(int id)
        {
            try
            {
                User user = db.Users.SqlQuery($"SELECT * FROM [User] WHERE BINARY_CHECKSUM(id) = BINARY_CHECKSUM('{id}')").First();
                List<Bill> bills = db.Bills.SqlQuery($"SELECT * FROM [Bill] WHERE BINARY_CHECKSUM(user_id) = BINARY_CHECKSUM('{id}') AND CAST([Bill].date as date) >= CAST(GETDATE()>30 as date)").ToList();
                user.Bills = bills;
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void DeleteUserById(int id)
        {
            db.Users.SqlQuery($"DELETE FROM [User] WHERE BINARY_CHECKSUM(id) = BINARY_CHECKSUM('{id}')");
        }

        public void EditUser(User newuser, int id)
        {
            var result = db.Users.SingleOrDefault(u => u.id == id);
            if (result != null)
            {
                result.email = newuser.email;
                result.password = newuser.password;
                result.username = newuser.username;
                result.avatar_url = newuser.avatar_url;
                result.role_id = newuser.role_id;
                db.SaveChanges();
            }
        }
        #endregion

        //Orderline DAO
        #region
        public List<Orderline> AddToCart(List<Orderline> list, int book_id, int quantity)
        {
            list.Add(new Orderline { book_id = book_id, quantity = quantity });
            return list;
        }
        #endregion
    }
}