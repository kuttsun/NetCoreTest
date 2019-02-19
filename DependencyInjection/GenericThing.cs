using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public abstract class GenericThing<T> : IThing<T>
    {
        public GenericThing()
        {
            GetName = typeof(T).Name;
            var guidValue = Guid.NewGuid();
            Console.WriteLine($"T={GetName}, GUID={guidValue}");
        }

        public string GetName { get; }
    }
}
