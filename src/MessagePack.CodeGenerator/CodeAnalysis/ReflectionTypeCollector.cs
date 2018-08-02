using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessagePack.CodeGenerator.CodeAnalysis;
using MessagePack.CodeGenerator.CodeAnalysis.Configuration;
using MessagePack.CodeGenerator.Utils;
using MessagePack.Configuration;
using Newtonsoft.Json;

namespace MessagePack.CodeGenerator
{
    public class ReflectionTypeCollector
    {
        private readonly string _assemblyFilePath;
        private readonly string _configFilePath;

        private HashSet<Type> _collectedTypes = new HashSet<Type>();
        private List<ObjectSerializationInfo> _collectedObjectInfo = new List<ObjectSerializationInfo>();
        private List<EnumSerializationInfo> _collectedEnumInfo = new List<EnumSerializationInfo>();
        private List<GenericSerializationInfo> _collectedGenericInfo = new List<GenericSerializationInfo>();
        private List<UnionSerializationInfo> _collectedUnionInfo = new List<UnionSerializationInfo>();
        private Dictionary<Type, ModelTypeConfiguration> _registeredTypeConfigs = new Dictionary<Type, ModelTypeConfiguration>();


        public ReflectionTypeCollector(string assemblyFilePath, string configFilePath)
        {
            _assemblyFilePath = assemblyFilePath;
            _configFilePath = configFilePath;
        }

        public CollectedInfo Collect()
        {
            var assembly = Assembly.UnsafeLoadFrom(_assemblyFilePath);

            var typeRegistrationType = assembly.DefinedTypes.Single(ti => ti.BaseType == typeof(TypeRegistration));
            var typeRegistration = (TypeRegistration) Activator.CreateInstance(typeRegistrationType);

            ReadAndUpdateConfiguration(typeRegistration);

            foreach (var type in _registeredTypeConfigs.Keys)
            {
                CollectType(type);
            }
            
            return new CollectedInfo
            {
                ObjectInfo = _collectedObjectInfo.ToArray(),
                UnionInfo = _collectedUnionInfo.ToArray(),
                EnumInfo = _collectedEnumInfo.ToArray(),
                GenericInfo = _collectedGenericInfo.ToArray()
            };
        }

        private void ReadAndUpdateConfiguration(TypeRegistration typeRegistration)
        {
            var config = ReadConfiguration();

            foreach (var type in typeRegistration.Types)
            {
                ModelTypeConfiguration modelTypeConfiguration;
                if (config.ModelConfigurations.ContainsKey(type.FullName))
                {
                    modelTypeConfiguration = config.ModelConfigurations[type.FullName];
                }
                else
                {
                    modelTypeConfiguration = new ModelTypeConfiguration();
                    config.ModelConfigurations[type.FullName] = modelTypeConfiguration;
                    modelTypeConfiguration.TypeId = config.GetNextTypeId();
                }

                UpdateTypeConfiguration(type, modelTypeConfiguration);
                modelTypeConfiguration.Type = type;
            }

            WriteConfiguration(config);

            _registeredTypeConfigs = BuildRegisteredTypesLookup(config);
        }

        private ConfigurationRoot ReadConfiguration()
        {
            if (!File.Exists(_configFilePath))
            {
                return new ConfigurationRoot();
            }

            var configString = File.ReadAllText(_configFilePath);
            return JsonConvert.DeserializeObject<ConfigurationRoot>(configString);
        }

        private void WriteConfiguration(ConfigurationRoot configuration)
        {
            var configString = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(_configFilePath, configString);
        }

        private void UpdateTypeConfiguration(Type type, ModelTypeConfiguration typeConfiguration)
        {
            var members = GetSerializableMembers(type);

            var previousMembers = new List<MemberConfiguration>(typeConfiguration.Members);

            foreach (var memberInfo in members)
            {
                if (!typeConfiguration.Members.Any(mc => MemberInfoEqualsConfig(memberInfo, mc)))
                {
                    var memberType = GetMemberInfoType(memberInfo);
                    var memberConfiguration = new MemberConfiguration
                    {
                        Name = memberInfo.Name,
                        TypeFullName = memberType.FullName,
                        Key = typeConfiguration.GetNextKey(),
                        MemberInfo = memberInfo
                    };

                    typeConfiguration.Members.Add(memberConfiguration);
                }
                else
                {
                    var memberConfiguration =
                        typeConfiguration.Members.Single(mc => MemberInfoEqualsConfig(memberInfo, mc));
                    memberConfiguration.MemberInfo = memberInfo;
                }
            }

            foreach (var memberConfiguration in previousMembers)
            {
                if (!members.Any(mi => MemberInfoEqualsConfig(mi, memberConfiguration)))
                {
                    typeConfiguration.Members.Remove(memberConfiguration);
                }
            }
        }

        private List<MemberInfo> GetSerializableMembers(Type type)
        {
            return type.GetMembers().Where(memberInfo =>
            {
                if (memberInfo is FieldInfo fieldInfo)
                {
                    return !fieldInfo.IsStatic;
                }
                if (memberInfo is PropertyInfo propertyInfo)
                {
                    return !propertyInfo.GetMethod.IsStatic;
                }
                return false;
            }).ToList();
        }

        private bool MemberInfoEqualsConfig(MemberInfo memberInfo, MemberConfiguration memberConfiguration)
        {
            if (memberInfo.Name != memberConfiguration.Name)
            {
                return false;
            }

            Type memberType = GetMemberInfoType(memberInfo);
            if (memberType == null)
            {
                return false;
            }

            return memberType.FullName == memberConfiguration.TypeFullName;
        }

        private Type GetMemberInfoType(MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetMethod.ReturnType;
            }

            return null;
        }

        private Dictionary<Type, ModelTypeConfiguration> BuildRegisteredTypesLookup(
            ConfigurationRoot config)
        {
            Dictionary<Type, ModelTypeConfiguration> dictionary = new Dictionary<Type, ModelTypeConfiguration>();

            foreach (var modelConfiguration in config.ModelConfigurations.Values)
            {
                dictionary[modelConfiguration.Type] = modelConfiguration;
            }

            return dictionary;
        }

        private void CollectType(Type type)
        {
            if (!_collectedTypes.Add(type))
            {
                return;
            }

            if (CollectorCore.EmbeddedTypes.Contains(type.FullName))
            {
                return;
            }

            if (type.IsArray)
            {
                CollectArray(type);
                return;
            }

            if (type.IsGenericType)
            {
                CollectGeneric(type);
                return;
            }

            CollectObject(type);
        }

        private void CollectObject(Type type)
        {
            var info = new ObjectSerializationInfo
            {
                Name = type.Name,
                FullName = type.FullName,
                ConstructorParameters = new MemberSerializationInfo[0],
                HasIMessagePackSerializationCallbackReceiver = false,
                IsClass = type.IsClass,
                IsIntKey = true,
                NeedsCastOnAfter = false,
                NeedsCastOnBefore = false
            };

            if (!_registeredTypeConfigs.ContainsKey(type))
            {
                info.DontSerialize = true;
            }
            else
            {
                var typeConfiguration = _registeredTypeConfigs[type];

                info.TypeId = typeConfiguration.TypeId;
                info.Members = typeConfiguration.Members.Select(this.CollectMemberSerializationInfo).ToArray();
            }

            _collectedObjectInfo.Add(info);
        }

        void CollectArray(Type type)
        {
            var elemType = type.GetElementType();
            CollectType(elemType);

            var info = new GenericSerializationInfo
            {
                FullName = type.FullName,
            };

            var rank = type.GetArrayRank();
            if (rank == 1)
            {
                info.FormatterName = $"global::MessagePack.Formatters.ArrayFormatter<{elemType.FullName}>";
            }
            else if (rank == 2)
            {
                info.FormatterName = $"global::MessagePack.Formatters.TwoDimentionalArrayFormatter<{elemType.FullName}>";
            }
            else if (rank == 3)
            {
                info.FormatterName = $"global::MessagePack.Formatters.ThreeDimentionalArrayFormatter<{elemType.FullName}>";
            }
            else if (rank == 4)
            {
                info.FormatterName = $"global::MessagePack.Formatters.FourDimentionalArrayFormatter<{elemType.FullName}>";
            }
            else
            {
                throw new InvalidOperationException("does not supports array dimention, " + info.FullName);
            }

            _collectedGenericInfo.Add(info);
        }

        void CollectGeneric(Type type)
        {
            var genericType = type.GetGenericTypeDefinition();
            var genericTypeString = genericType.FullName;
            var fullName = TypeNameFormatter.GetGenericTypeFullName(type);

            // special case
            if (fullName == "global::System.ArraySegment<byte>" || fullName == "global::System.ArraySegment<byte>?")
            {
                return;
            }

            // nullable
            if (genericTypeString == "T?")
            {
                CollectType(type.GenericTypeArguments[0]);

                if (!CollectorCore.EmbeddedTypes.Contains(type.GenericTypeArguments[0].ToString()))
                {
                    var info = new GenericSerializationInfo
                    {
                        FormatterName = $"global::MessagePack.Formatters.NullableFormatter<{type.GenericTypeArguments[0].FullName}>",
                        FullName = fullName,
                    };

                    _collectedGenericInfo.Add(info);
                }
                return;
            }

            // collection
            if (CollectorCore.KnownGenericTypes.TryGetValue(genericTypeString, out var formatter))
            {
                foreach (var item in type.GenericTypeArguments)
                {
                    CollectType(item);
                }

                var typeArgs = string.Join(", ", type.GenericTypeArguments.Select(x => x.FullName));
                var f = formatter.Replace("TREPLACE", typeArgs);

                var info = new GenericSerializationInfo
                {
                    FormatterName = f,
                    FullName = fullName,
                };

                _collectedGenericInfo.Add(info);

                if (genericTypeString == "System.Linq.ILookup`2")
                {
                    formatter = CollectorCore.KnownGenericTypes["System.Linq.IGrouping`2"];
                    f = formatter.Replace("TREPLACE", typeArgs);

                    var groupingInfo = new GenericSerializationInfo
                    {
                        FormatterName = f,
                        FullName = $"global::System.Linq.IGrouping<{typeArgs}>",
                    };

                    _collectedGenericInfo.Add(groupingInfo);

                    formatter = CollectorCore.KnownGenericTypes["System.Collections.Generic.IEnumerable`1"];
                    typeArgs = type.GenericTypeArguments[1].FullName;
                    f = formatter.Replace("TREPLACE", typeArgs);

                    var enumerableInfo = new GenericSerializationInfo
                    {
                        FormatterName = f,
                        FullName = $"global::System.Collections.Generic.IEnumerable<{typeArgs}>",
                    };

                    _collectedGenericInfo.Add(enumerableInfo);
                }
            }
        }

        private MemberSerializationInfo CollectMemberSerializationInfo(MemberConfiguration memberConfiguration)
        {
            var memberInfo = memberConfiguration.MemberInfo;
            var memberType = GetMemberInfoType(memberInfo);
            var info = new MemberSerializationInfo
            {
                IntKey = memberConfiguration.Key,
                Name = memberInfo.Name,
                Type = TypeNameFormatter.GetTypeFullName(memberType),
                ShortTypeName = memberType.Name,
                IsProperty = memberInfo.MemberType == MemberTypes.Property,
                IsField = memberInfo.MemberType == MemberTypes.Field
            };

            if (memberInfo is PropertyInfo propertyInfo)
            {
                info.IsReadable = propertyInfo.CanRead && propertyInfo.GetMethod.IsPublic;
                info.IsWritable = propertyInfo.CanWrite && propertyInfo.SetMethod.IsPublic;
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                info.IsReadable = fieldInfo.IsPublic;
                info.IsWritable = fieldInfo.IsPublic && !fieldInfo.IsInitOnly;
            }

            CollectType(memberType);

            return info;
        }
    }
}
