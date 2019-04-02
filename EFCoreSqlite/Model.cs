using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlite
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlitetest.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property<string>("AttributeCollection")
                .HasField("_attributes");
        }
    }

    public class Person
    {
        private readonly char delimiter = ';';
        public int Id { get; set; }
        public string Name { get; set; }
        string _attributes;
        [NotMapped]
        public int[] Attributes
        {
            get => _attributes.Split(delimiter).Select(x => int.Parse(x)).ToArray();
            set => _attributes = string.Join(delimiter, value);
        }
    }

    class Model1Test
    {
        public static void Run()
        {
            // データベースの作成
            using (var db = new PersonDbContext())
            {
                db.Database.EnsureCreated();
            }

            Insert("Hoge");
            Insert("Piyo");
            Read();

            Console.WriteLine("----------");

            //Update(1, "Foo");
            Read();

            Console.WriteLine("----------");

            //Delete(1);
            Read();
        }

        // レコードの追加
        static void Insert(string name)
        {
            using (var db = new PersonDbContext())
            {
                var person = new Person
                {
                    Name = name,
                    //Attributes = new string[] { "0", "1", "2" }
                    Attributes = new int[] { 0, 1, 2 }
                };
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
                    Console.WriteLine($"ID = {person.Id}, Name = {person.Name}, Attributes={string.Join(", ", person.Attributes)}");
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
