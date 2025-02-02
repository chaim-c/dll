using System;

namespace Mono.Cecil.PE
{
	// Token: 0x0200004D RID: 77
	internal sealed class TextMap
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000BA33 File Offset: 0x00009C33
		public void AddMap(TextSegment segment, int length)
		{
			this.map[(int)segment] = new Range(this.GetStart(segment), (uint)length);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BA53 File Offset: 0x00009C53
		public void AddMap(TextSegment segment, int length, int align)
		{
			align--;
			this.AddMap(segment, length + align & ~align);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BA67 File Offset: 0x00009C67
		public void AddMap(TextSegment segment, Range range)
		{
			this.map[(int)segment] = range;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BA7B File Offset: 0x00009C7B
		public Range GetRange(TextSegment segment)
		{
			return this.map[(int)segment];
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BA90 File Offset: 0x00009C90
		public DataDirectory GetDataDirectory(TextSegment segment)
		{
			Range range = this.map[(int)segment];
			return new DataDirectory((range.Length == 0U) ? 0U : range.Start, range.Length);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BACE File Offset: 0x00009CCE
		public uint GetRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public uint GetNextRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start + this.map[(int)segment].Length;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BB16 File Offset: 0x00009D16
		public int GetLength(TextSegment segment)
		{
			return (int)this.map[(int)segment].Length;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BB2C File Offset: 0x00009D2C
		private uint GetStart(TextSegment segment)
		{
			if (segment != TextSegment.ImportAddressTable)
			{
				return this.ComputeStart((int)segment);
			}
			return 8192U;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BB4B File Offset: 0x00009D4B
		private uint ComputeStart(int index)
		{
			index--;
			return this.map[index].Start + this.map[index].Length;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BB78 File Offset: 0x00009D78
		public uint GetLength()
		{
			Range range = this.map[15];
			return range.Start - 8192U + range.Length;
		}

		// Token: 0x04000375 RID: 885
		private readonly Range[] map = new Range[16];
	}
}
