using EntityFrameworkTutorial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkTutorial
{
    class Program
    {
        static void Main()
        {
            /*ContextExample();

            StatesExample();

            EagerLoadingExample();
            */

            LazyLoadingExample();
        }

        public static string GetTitle()
        {
            return "XYZ";
        }

        public static void ContextExample()
        {
            using (var context = new LibraryContext())
            {
                //creates db if not exists
                context.Database.EnsureCreated();

                //create entity objects
                var cat = new Category() { Name = "Fiction" };
                var author = new Author() { FirstName = "George", LastName = "Orwell" };
                var book1 = new Book() { Title = "XYZ", Author = author, Category = cat };
                var book2 = new Book() { Title = "Animal Farm", Author = author, Category = cat };


                //add entitiy to the context
                context.Books.Add(book1);
                context.Books.Add(book2);

                //save data to the database tables
                context.SaveChanges();

                //retrieve all the books from the database
                foreach (var b in context.Books)
                {
                    Console.WriteLine($"Title: {b.Title}");
                }
            }
        }

        static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()}");
            }
        }

        public static void StatesExample()
        {
            using (var context = new LibraryContext())
            {
                // retrieve entity 
                var book = context.Books.FirstOrDefault();
                DisplayStates(context.ChangeTracker.Entries());
            }

            using (var context = new LibraryContext())
            {
                context.Books.Add(new Book() { Title = "Alice's Adventures in Wonderland" });

                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        public static void QueryExample()
        {
            using (var context = new LibraryContext())
            {
                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .FirstOrDefault();

                Console.WriteLine($"Id: {book.BookId}");

            }
        }

        static void EagerLoadingExample()
        {

            using (var context = new LibraryContext())
            {
                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .Include(b => b.Category)
                    .FirstOrDefault();

                Console.WriteLine($"Title: {book.Title}, Category:{book.Category.Name}");
            }

                using (var context = new LibraryContext())
            {
                var category = context.Categories.Find(1);
                var location1 = new Location() { Name = "", Description = "", Category = category };
                category.Locations.Add(location1);
                context.SaveChanges();

                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .Include(b => b.Category)
                    .ThenInclude(c => c.Locations)
                    .FirstOrDefault();

                Console.Write($"Title: {book.Title}, Locations:");

                foreach (var loc in book.Category.Locations)
                    Console.Write(loc.Name + " ");

            }
        }

        static void LazyLoadingExample()
        {
            using (var context = new LibraryContext())
            {
                //Loading books only
                IList<Book> books = context.Books.ToList();

                Book b1 = books[0];

                //Loads author for particular book only (seperate SQL query)
                Author author = b1.Author;

                Console.Write(author.FirstName + " " + author.LastName);
            }
        }
    }
}
