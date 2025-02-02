using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200003D RID: 61
	internal class StringHeap : Heap
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00008038 File Offset: 0x00006238
		public StringHeap(Section section, uint start, uint size) : base(section, start, size)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008050 File Offset: 0x00006250
		public string Read(uint index)
		{
			if (index == 0U)
			{
				return string.Empty;
			}
			string text;
			if (this.strings.TryGetValue(index, out text))
			{
				return text;
			}
			if (index > this.Size - 1U)
			{
				return string.Empty;
			}
			text = this.ReadStringAt(index);
			if (text.Length != 0)
			{
				this.strings.Add(index, text);
			}
			return text;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000080A8 File Offset: 0x000062A8
		protected virtual string ReadStringAt(uint index)
		{
			int num = 0;
			byte[] data = this.Section.Data;
			int num2 = (int)(index + this.Offset);
			int num3 = num2;
			while (data[num3] != 0)
			{
				num++;
				num3++;
			}
			return Encoding.UTF8.GetString(data, num2, num);
		}

		// Token: 0x040002D8 RID: 728
		private readonly Dictionary<uint, string> strings = new Dictionary<uint, string>();
	}
}
