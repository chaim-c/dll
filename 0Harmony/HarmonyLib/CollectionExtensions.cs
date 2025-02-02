using System;
using System.Collections.Generic;
using System.Linq;

namespace HarmonyLib
{
	// Token: 0x0200004E RID: 78
	public static class CollectionExtensions
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00012ECC File Offset: 0x000110CC
		public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			if (sequence == null)
			{
				return;
			}
			foreach (T obj in sequence)
			{
				action(obj);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012EFA File Offset: 0x000110FA
		public static void DoIf<T>(this IEnumerable<T> sequence, Func<T, bool> condition, Action<T> action)
		{
			sequence.Where(condition).Do(action);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012F09 File Offset: 0x00011109
		public static IEnumerable<T> AddItem<T>(this IEnumerable<T> sequence, T item)
		{
			return (sequence ?? Array.Empty<T>()).Concat(new T[]
			{
				item
			});
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012F28 File Offset: 0x00011128
		public static T[] AddToArray<T>(this T[] sequence, T item)
		{
			return sequence.AddItem(item).ToArray<T>();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00012F38 File Offset: 0x00011138
		public static T[] AddRangeToArray<T>(this T[] sequence, T[] items)
		{
			return (sequence ?? Enumerable.Empty<T>()).Concat(items).ToArray<T>();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012F5C File Offset: 0x0001115C
		internal static Dictionary<K, V> Merge<K, V>(this IEnumerable<KeyValuePair<K, V>> firstDict, params IEnumerable<KeyValuePair<K, V>>[] otherDicts)
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			foreach (KeyValuePair<K, V> keyValuePair in firstDict)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (IEnumerable<KeyValuePair<K, V>> enumerable in otherDicts)
			{
				foreach (KeyValuePair<K, V> keyValuePair2 in enumerable)
				{
					dictionary[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013020 File Offset: 0x00011220
		internal static Dictionary<K, V> TransformKeys<K, V>(this Dictionary<K, V> origDict, Func<K, K> transform)
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			foreach (KeyValuePair<K, V> keyValuePair in origDict)
			{
				dictionary.Add(transform(keyValuePair.Key), keyValuePair.Value);
			}
			return dictionary;
		}
	}
}
