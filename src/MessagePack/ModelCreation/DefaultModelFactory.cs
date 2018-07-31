using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.ModelCreation
{
    public class DefaultModelFactory : IModelFactory
    {
        public T CreateModel<T>()
        {
            Console.WriteLine("Creating model of type {0}", typeof(T));
            return Activator.CreateInstance<T>();
        }
    }
}
