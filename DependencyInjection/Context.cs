using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjection
{
    public class Context : DbContext
    {
        readonly DbContextOptions<Context> _contextOption;

        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
            Console.WriteLine("Called Construct");
            _contextOption = options;

            // データベースが存在しない場合は新規作成
            Database.EnsureCreated();
        }

        public override void Dispose()
        {
            Console.WriteLine("Called Dispose");
            this.Dispose();
        }
    }
}
