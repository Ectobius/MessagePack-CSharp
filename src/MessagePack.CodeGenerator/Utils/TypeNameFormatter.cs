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
            string fullName = type.FullName;
            if (type.IsGenericType)
            {
                fullName = GetGenericTypeFullName(type);
            }

            return fullName.Replace('+', '.');
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
