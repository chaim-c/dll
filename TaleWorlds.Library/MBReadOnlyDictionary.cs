using System;
using System.Collections;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class MBReadOnlyDictionary<TKey, TValue> : ICollection, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000C24E File Offset: 0x0000A44E
		public MBReadOnlyDictionary(Dictionary<TKey, TValue> dictionary)
		{
			this._dictionary = dictionary;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C25D File Offset: 0x0000A45D
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C26A File Offset: 0x0000A46A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C26D File Offset: 0x0000A46D
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000C270 File Offset: 0x0000A470
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000C27D File Offset: 0x0000A47D
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000C28F File Offset: 0x0000A48F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C2AF File Offset: 0x0000A4AF
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x17000058 RID: 88
		public TValue this[TKey key]
		{
			get
			{
				return this._dictionary[key];
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000C2D9 File Offset: 0x0000A4D9
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public void CopyTo(Array array, int index)
		{
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				((ICollection)this._dictionary).CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (Dictionary<TKey, TValue>.Enumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this._dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				Debug.FailedAssert("Invalid array type", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\MBReadOnlyDictionary.cs", "CopyTo", 96);
			}
		}

		// Token: 0x04000118 RID: 280
		private Dictionary<TKey, TValue> _dictionary;
	}
}
