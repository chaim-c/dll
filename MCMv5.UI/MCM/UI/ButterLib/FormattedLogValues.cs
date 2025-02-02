using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MCM.UI.ButterLib
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	internal class FormattedLogValues : IReadOnlyList<KeyValuePair<string, object>>, IReadOnlyCollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008D91 File Offset: 0x00006F91
		[Nullable(2)]
		internal LogValuesFormatter Formatter
		{
			[NullableContext(2)]
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008D9C File Offset: 0x00006F9C
		[NullableContext(2)]
		public FormattedLogValues(string format, [Nullable(new byte[]
		{
			2,
			1
		})] params object[] values)
		{
			bool flag = (values == null || values.Length != 0) && format != null;
			if (flag)
			{
				bool flag2 = FormattedLogValues._count >= 1024;
				if (flag2)
				{
					bool flag3 = !FormattedLogValues._formatters.TryGetValue(format, out this._formatter);
					if (flag3)
					{
						this._formatter = new LogValuesFormatter(format);
					}
				}
				else
				{
					this._formatter = FormattedLogValues._formatters.GetOrAdd(format, delegate(string f)
					{
						Interlocked.Increment(ref FormattedLogValues._count);
						return new LogValuesFormatter(f);
					});
				}
			}
			this._originalMessage = (format ?? "[null]");
			this._values = values;
		}

		// Token: 0x1700008C RID: 140
		[Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		public KeyValuePair<string, object> this[int index]
		{
			[return: Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			get
			{
				bool flag = index < 0 || index >= this.Count;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				return (index == this.Count - 1) ? new KeyValuePair<string, object>("{OriginalFormat}", this._originalMessage) : this._formatter.GetValue(this._values, index);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008EB3 File Offset: 0x000070B3
		public int Count
		{
			get
			{
				return (this._formatter == null) ? 1 : (this._formatter.ValueNames.Count + 1);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008ED2 File Offset: 0x000070D2
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008EE1 File Offset: 0x000070E1
		public override string ToString()
		{
			return (this._formatter == null) ? this._originalMessage : this._formatter.Format(this._values);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008F04 File Offset: 0x00007104
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400007A RID: 122
		internal const int MaxCachedFormatters = 1024;

		// Token: 0x0400007B RID: 123
		private const string NullFormat = "[null]";

		// Token: 0x0400007C RID: 124
		private static int _count;

		// Token: 0x0400007D RID: 125
		private static ConcurrentDictionary<string, LogValuesFormatter> _formatters = new ConcurrentDictionary<string, LogValuesFormatter>();

		// Token: 0x0400007E RID: 126
		[Nullable(2)]
		private readonly LogValuesFormatter _formatter;

		// Token: 0x0400007F RID: 127
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly object[] _values;

		// Token: 0x04000080 RID: 128
		private readonly string _originalMessage;
	}
}
