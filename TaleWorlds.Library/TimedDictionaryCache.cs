using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TaleWorlds.Library
{
	// Token: 0x0200008E RID: 142
	public class TimedDictionaryCache<TKey, TValue>
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0001078D File Offset: 0x0000E98D
		public TimedDictionaryCache(long validMilliseconds)
		{
			this._dictionary = new Dictionary<TKey, ValueTuple<long, TValue>>();
			this._stopwatch = new Stopwatch();
			this._stopwatch.Start();
			this._validMilliseconds = validMilliseconds;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000107BD File Offset: 0x0000E9BD
		public TimedDictionaryCache(TimeSpan validTimeSpan) : this((long)validTimeSpan.TotalMilliseconds)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000107CD File Offset: 0x0000E9CD
		private bool IsItemExpired(TKey key)
		{
			return this._stopwatch.ElapsedMilliseconds - this._dictionary[key].Item1 >= this._validMilliseconds;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000107F7 File Offset: 0x0000E9F7
		private bool RemoveIfExpired(TKey key)
		{
			if (this.IsItemExpired(key))
			{
				this._dictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00010814 File Offset: 0x0000EA14
		public void PruneExpiredItems()
		{
			List<TKey> list = new List<TKey>();
			foreach (KeyValuePair<TKey, ValueTuple<long, TValue>> keyValuePair in this._dictionary)
			{
				if (this.IsItemExpired(keyValuePair.Key))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (TKey key in list)
			{
				this._dictionary.Remove(key);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000108C8 File Offset: 0x0000EAC8
		public void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000108D5 File Offset: 0x0000EAD5
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key) && !this.RemoveIfExpired(key);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000108F1 File Offset: 0x0000EAF1
		public bool Remove(TKey key)
		{
			this.RemoveIfExpired(key);
			return this._dictionary.Remove(key);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00010907 File Offset: 0x0000EB07
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this.ContainsKey(key))
			{
				value = this._dictionary[key].Item2;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x17000086 RID: 134
		public TValue this[TKey key]
		{
			get
			{
				this.RemoveIfExpired(key);
				return this._dictionary[key].Item2;
			}
			set
			{
				this._dictionary[key] = new ValueTuple<long, TValue>(this._stopwatch.ElapsedMilliseconds, value);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00010970 File Offset: 0x0000EB70
		public MBReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary()
		{
			this.PruneExpiredItems();
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, ValueTuple<long, TValue>> keyValuePair in this._dictionary)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value.Item2;
			}
			return dictionary.GetReadOnlyDictionary<TKey, TValue>();
		}

		// Token: 0x04000179 RID: 377
		[TupleElementNames(new string[]
		{
			"Timestamp",
			"Value"
		})]
		private readonly Dictionary<TKey, ValueTuple<long, TValue>> _dictionary;

		// Token: 0x0400017A RID: 378
		private readonly Stopwatch _stopwatch;

		// Token: 0x0400017B RID: 379
		private readonly long _validMilliseconds;
	}
}
