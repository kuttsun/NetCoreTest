using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlite
{
    class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlitetest2.db");
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
            using (var db = new BloggingContext())
            {
                db.Database.EnsureCreated();
            }


            //AddGraphOfNewEntities();
            //AddRelatedEntity();
            //ChangeRelationships();
            //RemoveRelationships();
            LoadAllData();
        }

        // 新規作成
        static void AddGraphOfNewEntities()
        {
            using (var context = new BloggingContext())
            {
                // Blogs テーブルに新規にレコードを追加し、その子として Posts に３つレコードを追加する
                var blog = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet",
                    Posts = new List<Post>
                    {
                        new Post { Title = "Intro to C#" },
                        new Post { Title = "Intro to VB.NET" },
                        new Post { Title = "Intro to F#" }
                    }
                };

                context.Blogs.Add(blog);
                context.SaveChanges();
            }
        }

        // 関連するエンティティの追加
        static void AddRelatedEntity()
        {
            using (var context = new BloggingContext())
            {
                // Blog テーブルの最初のレコードを取得し、
                var blog = context.Blogs.Include(b => b.Posts).First();
                // その子として Posts テーブルに新規にレコードを追加する
                var post = new Post { Title = "Intro to EF Core" };

                blog.Posts.Add(post);
                context.SaveChanges();
            }
        }

        // リレーションシップの変更
        static void ChangeRelationships()
        {
            using (var context = new BloggingContext())
            {
                // Blogs テーブルに新規にレコードを追加し、
                var blog = new Blog { Url = "http://blogs.msdn.com/visualstudio" };
                // Posts テーブルの最初のレコードの親を上記の Blogs に新規に追加するレコードに変更する
                var post = context.Posts.First();

                post.Blog = blog;
                context.SaveChanges();
            }
        }

        // リレーションシップの削除
        static void RemoveRelationships()
        {
            using (var context = new BloggingContext())
            {
                // Blog テーブルの最初のレコードを取得し、
                var blog = context.Blogs.Include(b => b.Posts).First();
                // その子の Posts テーブルから関係する最初のレコードを削除する
                var post = blog.Posts.First();

                blog.Posts.Remove(post);
                context.SaveChanges();
            }
        }

        static void LoadAllData()
        {
            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    //.Include(blog => blog.Posts)
                    .ToList();


                foreach (var blog in blogs)
                {
                    Console.WriteLine($"BlogId = {blog.BlogId}, Posts = {blog.Posts.Count()}");
                }
            }
        }
    }
}
