using System;

namespace EFCoreSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // データベースの作成
            using (var db = new PersonDbContext())
            {
                //if (db.Database.EnsureCreated())
                //{
                //    Console.WriteLine("Created");
                //}
                //else
                //{
                //    Console.WriteLine("Exist");
                //}
                db.Database.EnsureCreated();
            }

            try
            {
                Insert();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        // 新規作成
        static void Insert()
        {
            using (var context = new PersonDbContext())
            {
                var person = new Person
                {
                    Name = "Hoge",
                    Age = 30,
                    Gender = Gender.Male,
                    Birthplace = Birthplace.Japan
                };

                context.Persons.Add(person);
                context.SaveChanges();
            }
        }
    }
}
