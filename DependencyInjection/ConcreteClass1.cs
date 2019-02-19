using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class ConcreteClass1 : GenericThing<Class1>, IFoo
    {
        public ConcreteClass1()
        {

        }

        public string GetFoo() => "Foo";
    }
}
