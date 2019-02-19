using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection2
{
    class Class2
    {
        public Class2(IBar bar)
        {
            Console.WriteLine(bar.GetBar());
        }
    }
}
