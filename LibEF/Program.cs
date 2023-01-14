using LibEF.Exceptions;
using LibEF.Repositories;

namespace LibEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ur = new UserRepository();
            var br = new BookRepository();

            Book book = new Book 
            { 
                Name= "The Hobbit, or There and Back Again",
                Year= 1937
            };
            Book book3 = new Book
            {
                Name = "The Fellowship of the Ring",
                Year = 1954
            };
            Book book4 = new Book
            {
                Name = "The Two Towers",
                Year = 1954
            };
            Book book1 = new Book
            {
                Name = "Mistress of Mistresses",
                Year = 1935
            };
            Book book5 = new Book
            {
                Name = "A Fish Dinner in Memison",
                Year = 1941
            };
            Book book6 = new Book
            {
                Name = "The Mezentian Gate",
                Year = 1958
            };
            Book book2 = new Book
            {
                Name = "The X Factor",
                Year = 1965
            };
            Book book7 = new Book
            {
                Name = "The Beast Master",
                Year = 1959
            };
            Book book8 = new Book
            {
                Name = "Quag Keep",
                Year = 1978
            };

            Author author = new Author 
            {
                Name= "John",
                LastName= "Tolkien",
            };
            Author author1 = new Author
            {
                Name = "Eric",
                LastName = "Eddison",
            };

            Author author2 = new Author
            {
                Name = "Andre",
                LastName = "Norton",
            };

            Genre Genre = new Genre 
            {
                Name= "High fantasy"
            };
            Genre Genre1 = new Genre
            {
                Name = "Fantasy"
            };
            Genre Genre2 = new Genre
            {
                Name = "Science Fiction"
            };

            User user = new User 
            {
                Name="Petr",
                LastName="Jan",
                Email="petr@ya.ru",
                Address="Moscow"
            };
            User user1 = new User
            {
                Name = "Alex",
                LastName = "Nov",
                Email = "Alex@ya.ru",
                Address = "Tver"
            };
            User user2 = new User
            {
                Name = "Ivan",
                LastName = "Ivanov",
                Email = "Ivan@ya.ru",
                Address = "Riga"
            };

            book.Genres.Add(Genre);
            book1.Genres.Add(Genre1);
            book.Genres.Add(Genre1);
            book2.Genres.Add(Genre2);
            book3.Genres.Add(Genre1);
            book4.Genres.Add(Genre);
            book5.Genres.Add(Genre);
            book5.Genres.Add(Genre1);
            book6.Genres.Add(Genre);

            book7.Genres.Add(Genre2);
            book8.Genres.Add(Genre);
            book8.Genres.Add(Genre1);


            book.Authors.Add(author);
            book1.Authors.Add(author1);
            book2.Authors.Add(author2);
            book3.Authors.Add(author);
            book4.Authors.Add(author);
            book5.Authors.Add(author1);
            book6.Authors.Add(author1);
            book7.Authors.Add(author2);
            book8.Authors.Add(author2);


            book.User=user;
            book8.User = user;

            using (var db = new AppContext())
            {
                //пересоздаем базу
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //сохраняем созданные объекты в БД
                db.Authors.AddRange(author, author1, author2);
                db.Genres.AddRange(Genre, Genre1, Genre2);
                db.Book.AddRange(book, book1, book2, book3, book4, book5, book6, book7, book8);
                db.Users.AddRange(user, user1, user2);
                db.SaveChanges();
                Console.WriteLine("Initial data saved");
            }
                //подсчет книг по жанру
                var c = br.CountBooksByGenre(Genre2);
                Console.WriteLine($"Books by Genre {Genre.Name}: {c}");
                
                //подсчет книг по автору
                c = br.CountBooksByAuthor(author);
                Console.WriteLine($"Books by Author {author.Name} {author.LastName}: {c}");

                Console.WriteLine();
                //выборка книг по жанру и периоду
                var booksbyGenreYear = br.GetBooksByGenreYear(Genre2, 1959, 1975);
                Console.WriteLine($"Selected books:");
                foreach (var bo in booksbyGenreYear)
                {
                    Console.WriteLine($"{bo.Name} {bo.Year}");
                }
            //проверка методов возвращающих булевые значения. на консоль не выводится. результат проверю в дебаге
            var checkbook = br.CheckBookByAuthorAndName(author, book.Name);
            var checkbook1 = br.CheckBookByAuthorAndName(author, "a");

            bool chekBookUser = br.CheckIfBookUser(book, user);
            bool chekBookUser1 = br.CheckIfBookUser(book, user1);


            //получения списка книг выданных пользователю
            var booksByUser = br.GetBooksdByUser(user);
            Console.WriteLine("Books by User:");
            booksByUser.ForEach(b => Console.WriteLine(b.Name));
            Console.WriteLine();
            //получеине количества книг у пользователя
            Console.WriteLine("Count books by user:" + br.CountBooksByUser(user));
            //полуение последней по дате книги
            var lastBook = br.GetLastBook();
            Console.WriteLine("Last book by Year:" + lastBook.Name +" "+lastBook.Year);
            Console.WriteLine();
            //удаление книги из БД
            try
            {
                br.Delete(book);
                Console.WriteLine($"Book '{book.Name}' has been deleted");
            }
            catch (BookNotFoundException)
            {
                Console.WriteLine("Book not found");
            }
            Console.WriteLine();
            //пробуем удалить уже удаленнуб книгу
            try
            {
                br.Delete(book);
                Console.WriteLine($"Book '{book.Name}' has been deleted");
            }
            catch (BookNotFoundException)
            {
                Console.WriteLine("Book not found");
            }
            Console.WriteLine();
            //выводим список всех книг с соритровкой по названию, затем по году выхода.
            var orderedBooks = br.GetAllBooksOrderedByName();
            var orderedBooks1 = br.GetAllBooksOrderedByYear();

            orderedBooks.ForEach(b => Console.WriteLine(b.Name +" "+ b.Year));
            Console.WriteLine();
            orderedBooks1.ForEach(b => Console.WriteLine(b.Name + " "+b.Year));

            
        }
    }
}