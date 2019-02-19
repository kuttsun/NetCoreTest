using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class Class2
    {
        readonly IThing _thing;

        public Class2(IThing<Class2> thing)
        {
            _thing = thing;
        }
    }
}
