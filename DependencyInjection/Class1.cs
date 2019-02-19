using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class Class1
    {
        readonly IThing _thing;

        public Class1(IThing<Class1> thing)
        {
            _thing = thing;
        }
    }
}
