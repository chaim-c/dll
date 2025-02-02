using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCM.UI.ButterLib
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	internal class LogValuesFormatter
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00008A14 File Offset: 0x00006C14
		public LogValuesFormatter(string format)
		{
			this.OriginalFormat = format;
			StringBuilder stringBuilder = new StringBuilder();
			int startIndex = 0;
			int length = format.Length;
			while (startIndex < length)
			{
				int braceIndex = LogValuesFormatter.FindBraceIndex(format, '{', startIndex, length);
				int braceIndex2 = LogValuesFormatter.FindBraceIndex(format, '}', braceIndex, length);
				int indexOf = LogValuesFormatter.FindIndexOf(format, ',', braceIndex, braceIndex2);
				bool flag = indexOf == braceIndex2;
				if (flag)
				{
					indexOf = LogValuesFormatter.FindIndexOf(format, ':', braceIndex, braceIndex2);
				}
				bool flag2 = braceIndex2 == length;
				if (flag2)
				{
					stringBuilder.Append(format, startIndex, length - startIndex);
					startIndex = length;
				}
				else
				{
					stringBuilder.Append(format, startIndex, braceIndex - startIndex + 1);
					stringBuilder.Append(this._valueNames.Count.ToString(CultureInfo.InvariantCulture));
					this._valueNames.Add(format.Substring(braceIndex + 1, indexOf - braceIndex - 1));
					stringBuilder.Append(format, indexOf, braceIndex2 - indexOf + 1);
					startIndex = braceIndex2 + 1;
				}
			}
			this._format = stringBuilder.ToString();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008B26 File Offset: 0x00006D26
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008B2E File Offset: 0x00006D2E
		public string OriginalFormat { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008B37 File Offset: 0x00006D37
		public List<string> ValueNames
		{
			get
			{
				return this._valueNames;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008B40 File Offset: 0x00006D40
		private static int FindBraceIndex(string format, char brace, int startIndex, int endIndex)
		{
			int braceIndex = endIndex;
			int index = startIndex;
			int num = 0;
			while (index < endIndex)
			{
				bool flag = num > 0 && format[index] != brace;
				if (flag)
				{
					bool flag2 = num % 2 == 0;
					if (!flag2)
					{
						break;
					}
					num = 0;
					braceIndex = endIndex;
				}
				else
				{
					bool flag3 = format[index] == brace;
					if (flag3)
					{
						bool flag4 = brace == '}';
						if (flag4)
						{
							bool flag5 = num == 0;
							if (flag5)
							{
								braceIndex = index;
							}
						}
						else
						{
							braceIndex = index;
						}
						num++;
					}
				}
				index++;
			}
			return braceIndex;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008BD4 File Offset: 0x00006DD4
		private static int FindIndexOf(string format, char ch, int startIndex, int endIndex)
		{
			int num = format.IndexOf(ch, startIndex, endIndex - startIndex);
			return (num != -1) ? num : endIndex;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008BFC File Offset: 0x00006DFC
		public string Format([Nullable(new byte[]
		{
			2,
			1
		})] object[] values)
		{
			bool flag = values != null;
			if (flag)
			{
				for (int index = 0; index < values.Length; index++)
				{
					object obj = values[index];
					object obj2 = obj;
					if (obj2 != null)
					{
						IEnumerable source = obj2 as IEnumerable;
						if (source != null)
						{
							values[index] = string.Join<object>(", ", from object o in source
							select o ?? "(null)");
						}
					}
					else
					{
						values[index] = "(null)";
					}
				}
			}
			return string.Format(CultureInfo.InvariantCulture, this._format, values ?? LogValuesFormatter.EmptyArray);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008CA8 File Offset: 0x00006EA8
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		public KeyValuePair<string, object> GetValue(object[] values, int index)
		{
			bool flag = index < 0 || index > this._valueNames.Count;
			if (flag)
			{
				throw new IndexOutOfRangeException("index");
			}
			return (this._valueNames.Count > index) ? new KeyValuePair<string, object>(this._valueNames[index], values[index]) : new KeyValuePair<string, object>("{OriginalFormat}", this.OriginalFormat);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008D14 File Offset: 0x00006F14
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		public IEnumerable<KeyValuePair<string, object>> GetValues(object[] values)
		{
			KeyValuePair<string, object>[] values2 = new KeyValuePair<string, object>[values.Length + 1];
			for (int index = 0; index != this._valueNames.Count; index++)
			{
				values2[index] = new KeyValuePair<string, object>(this._valueNames[index], values[index]);
			}
			values2[values2.Length - 1] = new KeyValuePair<string, object>("{OriginalFormat}", this.OriginalFormat);
			return values2;
		}

		// Token: 0x04000075 RID: 117
		private const string NullValue = "(null)";

		// Token: 0x04000076 RID: 118
		private static readonly object[] EmptyArray = Array.Empty<object>();

		// Token: 0x04000077 RID: 119
		private readonly string _format;

		// Token: 0x04000078 RID: 120
		private readonly List<string> _valueNames = new List<string>();
	}
}
