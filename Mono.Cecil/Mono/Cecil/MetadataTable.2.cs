using System;

namespace Mono.Cecil
{
	// Token: 0x02000068 RID: 104
	internal abstract class MetadataTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000110BA File Offset: 0x0000F2BA
		public sealed override int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public int AddRow(TRow row)
		{
			if (this.rows.Length == this.length)
			{
				this.Grow();
			}
			this.rows[this.length++] = row;
			return this.length;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001110C File Offset: 0x0000F30C
		private void Grow()
		{
			TRow[] destinationArray = new TRow[this.rows.Length * 2];
			Array.Copy(this.rows, destinationArray, this.rows.Length);
			this.rows = destinationArray;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011144 File Offset: 0x0000F344
		public override void Sort()
		{
		}

		// Token: 0x040003AE RID: 942
		internal TRow[] rows = new TRow[2];

		// Token: 0x040003AF RID: 943
		internal int length;
	}
}
