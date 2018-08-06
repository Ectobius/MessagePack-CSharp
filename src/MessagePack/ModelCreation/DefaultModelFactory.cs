using System;

namespace MessagePack.ModelCreation
{
    public class DefaultModelFactory : IModelFactory
    {
        public T CreateModel<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
