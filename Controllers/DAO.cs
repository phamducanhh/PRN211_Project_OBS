﻿using System;
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
            string sql = "select b.id, b.title, b.image_url, b.author_id, b.[description], b.price, count(b.id) from (Book b inner join Book_Genre bg on (b.id = bg.book_id)) where ";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1) sql += $"genre_id = {arr[i]} ";
                else sql += $"genre_id = {arr[i]} or ";
            }
            sql += $"group by b.id, b.title, b.image_url, b.author_id, b.[description], b.price having COUNT(b.id) = {arr.Length}";
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
            if (orderline==null)
            {
                list.Add(new Orderline { book_id = book_id, quantity = quantity });
                return list;
            }
            orderline.quantity = orderline.quantity + quantity;
            return list;
        }

        public List<Orderline> RemoveFromCart(List<Orderline> list, int book_id)
        {
            list.Remove(GetOrderlineByBookId(list,book_id));
            return list;
        }

        public void AddOrderline(Orderline orderline)
        {
            db.Orderlines.Add(new Orderline { bill_id = IDOfNewBill(), book_id = orderline.book_id, quantity = orderline.quantity });
            db.SaveChanges();
        }
        #endregion

        //Bill DAO
        #region
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
        public List<Bill> GetAllBill()
        {
            string sql = "select *from Bill";
            return db.Bills.SqlQuery(sql).ToList();
        }
         public void ChangeStatus(int id,string status)
        {
            var change = db.Bills.SingleOrDefault(c => c.id == id);
            change.status = status;
            db.SaveChanges();
        }
        public List<Orderline> GetBillDeltai(int id)
        {
           
            return db.Orderlines.SqlQuery($"select *from OrderLine where bill_id ={id}").ToList();
        }
        public List<Bill> GetUnseenBill()
        {
            string sql = "select * from Bill where status = 'Unseen'";
            return db.Bills.SqlQuery(sql).ToList();
        }
        public void DeleteBill( int id)
        {
            var ListBillDetail = GetBillDeltai(id);
            foreach( var item in ListBillDetail)
            {
                db.Orderlines.Remove(item);
            }
            var change = db.Bills.SingleOrDefault(c => c.id == id);
            db.Bills.Remove(change);
            db.SaveChanges();

        }
        public void UpdateStockBook( int BookID, int Quan)
        {
            var change = db.Stocks.SingleOrDefault(c => c.book_id == BookID);
            change.quantity -= Quan;
            db.SaveChanges();
        }
        #endregion

    }
}