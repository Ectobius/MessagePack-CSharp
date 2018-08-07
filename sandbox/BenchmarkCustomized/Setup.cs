using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkCustomized.Resolvers;
using MessagePack.Resolvers;

namespace BenchmarkCustomized
{
    public class Setup
    {
        public static void RegisterResolvers()
        {
            MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
                // use generated resolver first, and combine many other generated/custom resolvers
                Resolvers.GeneratedResolver.Instance,

                // finally, use builtin/primitive resolver(don't use StandardResolver, it includes dynamic generation)
                MessagePack.Resolvers.BuiltinResolver.Instance,
                MessagePack.Resolvers.AttributeFormatterResolver.Instance,
                MessagePack.Resolvers.PrimitiveObjectResolver.Instance
            );
        }
    }
}
