using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Extensions
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal static class DictionaryExtensions
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000B022 File Offset: 0x00009222
		public static void Deconstruct<[Nullable(2)] TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			0,
			1,
			1
		})] this KeyValuePair<TKey, TValue> tuple, out TKey key, out TValue value)
		{
			key = tuple.Key;
			value = tuple.Value;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B040 File Offset: 0x00009240
		public static void AddRangeCautiously<[Nullable(2)] TKey, [Nullable(2)] TValue>(this Dictionary<TKey, TValue> destinationDict, Dictionary<TKey, TValue> otherDict, bool overrideDuplicates = false)
		{
			if (destinationDict == null)
			{
				destinationDict = new Dictionary<TKey, TValue>();
			}
			if (otherDict == null)
			{
				otherDict = new Dictionary<TKey, TValue>();
			}
			foreach (KeyValuePair<TKey, TValue> entry in otherDict)
			{
				bool flag = !destinationDict.ContainsKey(entry.Key);
				if (flag)
				{
					destinationDict.Add(entry.Key, entry.Value);
				}
				else if (overrideDuplicates)
				{
					destinationDict[entry.Key] = entry.Value;
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B0EC File Offset: 0x000092EC
		public static void AddRange<[Nullable(2)] TKey, [Nullable(2)] TValue>(this Dictionary<TKey, TValue> destinationDict, Dictionary<TKey, TValue> otherDict)
		{
			foreach (KeyValuePair<TKey, TValue> entry in otherDict)
			{
				destinationDict.Add(entry.Key, entry.Value);
			}
		}
	}
}
