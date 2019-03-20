using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class Hoge
    {
        readonly Context _context;

        public Hoge(Context context)
        {
            _context = context;
        }

        public void Insert()
        {
            _context.Persons.Add(new Person());
            _context.SaveChanges();
        }
    }
}
