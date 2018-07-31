using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.ModelCreation
{
    public interface IModelFactory
    {
        T CreateModel<T>();
    }
}
