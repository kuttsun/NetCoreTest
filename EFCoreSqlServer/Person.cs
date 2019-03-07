using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreSqlServer
{
    enum Gender
    {
        Male,
        Female,
        Unknown
    }

    enum Birthplace
    {
        Japan
    }

    class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Birthplace Birthplace { get; set; }
    }
}
