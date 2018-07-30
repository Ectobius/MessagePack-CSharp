using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePack.CodeGenerator.CodeAnalysis
{
    internal class CollectorCore
    {
        internal static readonly HashSet<string> EmbeddedTypes = new HashSet<string>(new string[]
        {
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.UInt16",
            "System.UInt32",
            "System.UInt64",
            "System.Single",
            "System.Double",
            "System.Boolean",
            "System.Byte",
            "System.SByte",
            "System.Decimal",
            "System.Char",
            "System.String",
            "System.Object",

            "System.Guid",
            "System.TimeSpan",
            "System.DateTime",
            "System.DateTimeOffset",

            "MessagePack.Nil",

            // and arrays
            
            "short[]",
            "int[]",
            "long[]",
            "ushort[]",
            "uint[]",
            "ulong[]",
            "float[]",
            "double[]",
            "bool[]",
            "byte[]",
            "sbyte[]",
            "decimal[]",
            "char[]",
            "string[]",
            "System.DateTime[]",
            "System.ArraySegment<byte>",
            "System.ArraySegment<byte>?",

            // extensions

            "UnityEngine.Vector2",
            "UnityEngine.Vector3",
            "UnityEngine.Vector4",
            "UnityEngine.Quaternion",
            "UnityEngine.Color",
            "UnityEngine.Bounds",
            "UnityEngine.Rect",
            "UnityEngine.AnimationCurve",
            "UnityEngine.RectOffset",
            "UnityEngine.Gradient",
            "UnityEngine.WrapMode",
            "UnityEngine.GradientMode",
            "UnityEngine.Keyframe",
            "UnityEngine.Matrix4x4",
            "UnityEngine.GradientColorKey",
            "UnityEngine.GradientAlphaKey",
            "UnityEngine.Color32",
            "UnityEngine.LayerMask",
            "UnityEngine.Vector2Int",
            "UnityEngine.Vector3Int",
            "UnityEngine.RangeInt",
            "UnityEngine.RectInt",
            "UnityEngine.BoundsInt",

            "System.Reactive.Unit",
        });

        internal static readonly Dictionary<string, string> KnownGenericTypes = new Dictionary<string, string>
        {
            {"System.Collections.Generic.List`1", "global::MessagePack.Formatters.ListFormatter<TREPLACE>" },
            {"System.Collections.Generic.LinkedList`1", "global::MessagePack.Formatters.LinkedListFormatter<TREPLACE>"},
            {"System.Collections.Generic.Queue`1", "global::MessagePack.Formatters.QeueueFormatter<TREPLACE>"},
            {"System.Collections.Generic.Stack`1", "global::MessagePack.Formatters.StackFormatter<TREPLACE>"},
            {"System.Collections.Generic.HashSet`1", "global::MessagePack.Formatters.HashSetFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyCollection`1", "global::MessagePack.Formatters.ReadOnlyCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.IList`1", "global::MessagePack.Formatters.InterfaceListFormatter<TREPLACE>"},
            {"System.Collections.Generic.ICollection`1", "global::MessagePack.Formatters.InterfaceCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.IEnumerable`1", "global::MessagePack.Formatters.InterfaceEnumerableFormatter<TREPLACE>"},
            {"System.Collections.Generic.Dictionary`2", "global::MessagePack.Formatters.DictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.IDictionary`2", "global::MessagePack.Formatters.InterfaceDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.SortedDictionary`2", "global::MessagePack.Formatters.SortedDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.SortedList`2", "global::MessagePack.Formatters.SortedListFormatter<TREPLACE>"},
            {"System.Linq.ILookup`2", "global::MessagePack.Formatters.InterfaceLookupFormatter<TREPLACE>"},
            {"System.Linq.IGrouping`2", "global::MessagePack.Formatters.InterfaceGroupingFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ObservableCollection`1", "global::MessagePack.Formatters.ObservableCollectionFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyObservableCollection`1", "global::MessagePack.Formatters.ReadOnlyObservableCollectionFormatter<TREPLACE>" },
            {"System.Collections.Generic.IReadOnlyList`1", "global::MessagePack.Formatters.InterfaceReadOnlyListFormatter<TREPLACE>"},
            {"System.Collections.Generic.IReadOnlyCollection`1", "global::MessagePack.Formatters.InterfaceReadOnlyCollectionFormatter<TREPLACE>"},
            {"System.Collections.Generic.ISet`1", "global::MessagePack.Formatters.InterfaceSetFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentBag`1", "global::MessagePack.Formatters.ConcurrentBagFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentQueue`1", "global::MessagePack.Formatters.ConcurrentQueueFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentStack`1", "global::MessagePack.Formatters.ConcurrentStackFormatter<TREPLACE>"},
            {"System.Collections.ObjectModel.ReadOnlyDictionary`2", "global::MessagePack.Formatters.ReadOnlyDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Generic.IReadOnlyDictionary`2", "global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Concurrent.ConcurrentDictionary`2", "global::MessagePack.Formatters.ConcurrentDictionaryFormatter<TREPLACE>"},
            {"System.Lazy`1", "global::MessagePack.Formatters.LazyFormatter<TREPLACE>"},
            {"System.Threading.Tasks`1", "global::MessagePack.Formatters.TaskValueFormatter<TREPLACE>"},

            {"System.Tuple`1", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`2", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`3", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`4", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`5", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`6", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`7", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},
            {"System.Tuple`8", "global::MessagePack.Formatters.TupleFormatter<TREPLACE>"},

            {"System.ValueTuple`1", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`2", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`3", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`4", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`5", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`6", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`7", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},
            {"System.ValueTuple`8", "global::MessagePack.Formatters.ValueTupleFormatter<TREPLACE>"},

            {"System.Collections.Generic.KeyValuePair`2", "global::MessagePack.Formatters.KeyValuePairFormatter<TREPLACE>"},
            {"System.Threading.Tasks.ValueTask`1", "global::MessagePack.Formatters.KeyValuePairFormatter<TREPLACE>"},
            {"System.ArraySegment`1", "global::MessagePack.Formatters.ArraySegmentFormatter<TREPLACE>"},

            // extensions

            {"System.Collections.Immutable.ImmutableArray`1", "global::MessagePack.ImmutableCollection.ImmutableArrayFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableList`1", "global::MessagePack.ImmutableCollection.ImmutableListFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableDictionary`2", "global::MessagePack.ImmutableCollection.ImmutableDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableHashSet`1", "global::MessagePack.ImmutableCollection.ImmutableHashSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableSortedDictionary`2", "global::MessagePack.ImmutableCollection.ImmutableSortedDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableSortedSet`1", "global::MessagePack.ImmutableCollection.ImmutableSortedSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableQueue`1", "global::MessagePack.ImmutableCollection.ImmutableQueueFormatter<TREPLACE>"},
            {"System.Collections.Immutable.ImmutableStack`1", "global::MessagePack.ImmutableCollection.ImmutableStackFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableList`1", "global::MessagePack.ImmutableCollection.InterfaceImmutableListFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableDictionary`2", "global::MessagePack.ImmutableCollection.InterfaceImmutableDictionaryFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableQueue`1", "global::MessagePack.ImmutableCollection.InterfaceImmutableQueueFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableSet`1", "global::MessagePack.ImmutableCollection.InterfaceImmutableSetFormatter<TREPLACE>"},
            {"System.Collections.Immutable.IImmutableStack`1", "global::MessagePack.ImmutableCollection.InterfaceImmutableStackFormatter<TREPLACE>"},

            {"Reactive.Bindings.ReactiveProperty`1", "global::MessagePack.ReactivePropertyExtension.ReactivePropertyFormatter<TREPLACE>"},
            {"Reactive.Bindings.IReactiveProperty`1", "global::MessagePack.ReactivePropertyExtension.InterfaceReactivePropertyFormatter<TREPLACE>"},
            {"Reactive.Bindings.IReadOnlyReactiveProperty`1", "global::MessagePack.ReactivePropertyExtension.InterfaceReadOnlyReactivePropertyFormatter<TREPLACE>"},
            {"Reactive.Bindings.ReactiveCollection`1", "global::MessagePack.ReactivePropertyExtension.ReactiveCollectionFormatter<TREPLACE>"},
        };
    }
}
