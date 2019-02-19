using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection2
{
    class Class1
    {
        public Class1(IFoo foo)
        {
            Console.WriteLine(foo.GetFoo());
        }
    }
}
