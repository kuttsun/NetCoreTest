using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection2
{
    class ImplClass : IFoo, IBar
    {
        public ImplClass()
        {
            var guid = Guid.NewGuid();
            Console.WriteLine($"GUID={guid}");
        }

        public string GetFoo() => "Foo";
        public string GetBar() => "Bar";
    }
}
