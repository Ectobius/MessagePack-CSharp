using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePack.CodeGenerator.Utils
{
    public class TypeNameFormatter
    {
        public static string GetTypeFullName(Type type)
        {
            if (type.IsGenericType)
            {
                return GetGenericTypeFullName(type);
            }

            return type.FullName;
        }

        public static string GetGenericTypeFullName(Type type)
        {
            var genericType = type.GetGenericTypeDefinition();
            var fullName = genericType.FullName;

            fullName = fullName.Split('`')[0];

            var typeArguments = String.Join(", ", type.GenericTypeArguments.Select(arg => arg.FullName));

            return $"{fullName}<{typeArguments}>";
        }
    }
}
