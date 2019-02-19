using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class DiClass1
    {
        public DiClass1(IThing<Class1> _thing, IFoo foo)
        {
            Console.WriteLine($"DIed {_thing.GetName}");
            Console.WriteLine($"{foo.GetFoo()}");
        }
    }
}
