using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlite
{
    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlitetest2.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .HasConstraintName("ForeignKey_Post_Blog");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    class Model2Test
    {
        public static void Run()
        {
            // データベースの作成
            using (var db = new MyContext())
            {
                db.Database.EnsureCreated();
            }

            Insert("Hoge");
            Insert("Piyo");
            Read();

            Update(1, "Foo");
            Read();

            Delete(1);
            Read();
        }

        // レコードの追加
        static void Insert(string name)
        {
            using (var db = new PersonDbContext())
            {
                var person = new Person { Name = name };
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        // レコードの取得
        static void Read()
        {
            using (var db = new PersonDbContext())
            {
                Console.WriteLine("----------");
                foreach (var person in db.Persons)
                {
                    Console.WriteLine($"ID = {person.Id}, Name = {person.Name}");
                }
            }
        }

        // レコードの更新
        static void Update(int id, string name)
        {
            using (var db = new PersonDbContext())
            {
                var person = db.Persons.Where(x => x.Id == id).FirstOrDefault();
                person.Name = name;
                db.SaveChanges();
            }
        }

        // レコードの削除
        static void Delete(int id)
        {
            using (var db = new PersonDbContext())
            {
                var person = db.Persons.Where(x => x.Id == id).FirstOrDefault();
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }
    }
}
