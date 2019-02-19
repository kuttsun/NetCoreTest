using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IThing<T> : IThing
    {
    }

    public interface IThing
    {
        string GetName { get; }
    }
}
