using LibEF.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEF.Repositories
{
    public class BookRepository
    {
        public Book GetBookById(int id)
        {
            using (var db = new AppContext())
            {
                Book book = db.Book.FirstOrDefault(b=>b.Id==id);
                return book;
            }
        }
        public List<Book> GetAllBooks()
        {
            using(var db = new AppContext())
            {
                var books = db.Book;
                return books.ToList();
            }    
        }
        public void Delete (Book book)
        {
            using(var db=new AppContext())
            {
                if (GetBookById(book.Id) is Book)
                {
                    db.Book.Remove(book);
                    db.SaveChanges();
                }
                else throw new BookNotFoundException();
            }
        }
        public void Add(Book book)
        {
            using(var db=new AppContext())
            {
                db.Book.Add(book);
                db.SaveChanges();
            }
        }
        public void ChangeYearById(int id, int year)
        {
            var book = GetBookById(id);
            if (book!=null)
            {
                using (var db = new AppContext())
                {
                    book.Year = year;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

        }
        public List<Book> GetBooksByGenreYear(Genre genre, int from, int to)
        {
            using (var db=new AppContext())
            {
                var books = db.Book.Where(b => b.Genres.Contains(genre) && b.Year >= from && b.Year <= to);
                return books.ToList();
            }
        }
        public int CountBooksByAuthor(Author author)
        {
            using (var db = new AppContext())
            {
                var books = db.Book.Where(b => b.Authors.Contains(author)).Count();
                return books;
            }
        }
        public int CountBooksByGenre(Genre genre)
        {
            using(var db = new AppContext())
            {
                var books = db.Book.Where(b => b.Genres.Contains(genre)).Count();
                return books;
            }
        }

        public bool CheckBookByAuthorAndName(Author author, string name)
        {
            using (var db = new AppContext())
            {
                var books = db.Book.Where(b => b.Authors.Contains(author) && b.Name == name).ToList() ;
                return books.Count != 0;
            }
        }
        public bool CheckIfBookUser(Book book,User user)
        {
            using (var db = new AppContext())
            {
                var books = db.Book.Where(b => b.Id == book.Id && b.UserId == user.Id).ToList();
                return books.Count != 0;
            }
        }

        public List<Book> GetBooksdByUser(User user)
        {
            using(var db = new AppContext())
            {
                var books = db.Book.Where(b => b.UserId == user.Id).ToList();
                return books;
            }
        }

        public int CountBooksByUser(User user)
        {
            using (var db = new AppContext())
            {
                var books=db.Book.Where(b => b.UserId == user.Id).ToList().Count;
                return books;
            }
        }

        public Book GetLastBook()
        {
            using (var db = new AppContext())
            {
                var book = db.Book.OrderByDescending(b => b.Year);
                return book.First();
            }
        }
        public List<Book> GetAllBooksOrderedByName()
        {
            using (var db = new AppContext())
            {
                var books = db.Book.OrderBy(b => b.Name);
                return books.ToList();
            }
        }
        public List<Book> GetAllBooksOrderedByYear()
        {
            using (var db = new AppContext())
            {
                return db.Book.OrderByDescending(b => b.Year).ToList();
            }
        }
    }
}
