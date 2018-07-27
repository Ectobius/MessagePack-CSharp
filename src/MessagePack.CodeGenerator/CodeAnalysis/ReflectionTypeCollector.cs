using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessagePack.CodeGenerator.CodeAnalysis.Configuration;
using MessagePack.Configuration;
using Newtonsoft.Json;

namespace MessagePack.CodeGenerator
{
    public class ReflectionTypeCollector
    {
        private readonly string _assemblyFilePath;
        private readonly string _configFilePath;

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

            var config = ReadAndUpdateConfiguration(typeRegistration);

            var objectSerializationInfos = config.Select(pair => GetObjectSerializationInfo(pair.Value));

            return new CollectedInfo
            {
                ObjectInfo = objectSerializationInfos.ToArray(),
                UnionInfo = new UnionSerializationInfo[0],
                EnumInfo = new EnumSerializationInfo[0],
                GenericInfo = new GenericSerializationInfo[0]
            };
        }

        private Dictionary<string, ModelTypeConfiguration> ReadAndUpdateConfiguration(TypeRegistration typeRegistration)
        {
            var config = ReadConfiguration();

            foreach (var type in typeRegistration.Types)
            {
                ModelTypeConfiguration typeConfiguration;
                if (config.ContainsKey(type.FullName))
                {
                    typeConfiguration = config[type.FullName];
                }
                else
                {
                    typeConfiguration = new ModelTypeConfiguration();
                    config[type.FullName] = typeConfiguration;
                }

                UpdateTypeConfiguration(type, typeConfiguration);
                typeConfiguration.Type = type;
            }

            WriteConfiguration(config);

            return config;
        }

        private Dictionary<string, ModelTypeConfiguration> ReadConfiguration()
        {
            if (!File.Exists(_configFilePath))
            {
                return new Dictionary<string, ModelTypeConfiguration>();
            }

            var configString = File.ReadAllText(_configFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, ModelTypeConfiguration>>(configString);
        }

        private void WriteConfiguration(Dictionary<string, ModelTypeConfiguration> configuration)
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

        private ObjectSerializationInfo GetObjectSerializationInfo(ModelTypeConfiguration configuration)
        {
            var type = configuration.Type;
            var info = new ObjectSerializationInfo
            {
                Name = type.Name,
                FullName = type.FullName,
                ConstructorParameters = new MemberSerializationInfo[0],
                HasIMessagePackSerializationCallbackReceiver = false,
                IsClass = type.IsClass,
                IsIntKey = true,
                NeedsCastOnAfter = false,
                NeedsCastOnBefore = false,
                Members = configuration.Members.Select(this.GetMemberSerializationInfo).ToArray()
            };

            return info;
        }

        private MemberSerializationInfo GetMemberSerializationInfo(MemberConfiguration memberConfiguration)
        {
            var memberInfo = memberConfiguration.MemberInfo;
            var memberType = GetMemberInfoType(memberInfo);
            var info = new MemberSerializationInfo
            {
                IntKey = memberConfiguration.Key,
                Name = memberInfo.Name,
                Type = memberType.FullName,
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

            return info;
        }
    }
}
