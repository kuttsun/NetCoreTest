using System;
using System.Linq;

namespace EFCoreSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            // データベースの作成
            using (var db = new PersonDbContext())
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

            Console.ReadKey();
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
