using System;
using System.Collections.Generic;

namespace MessagePack.Configuration
{
    public class TypeRegistration
    {
        public IReadOnlyList<Type> Types { get; private set; }

        protected void Register(params Type[] types)
        {
            Types = new List<Type>(types).AsReadOnly();
        }
    }
}
