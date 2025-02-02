using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200002C RID: 44
	internal sealed class TableHeapBuffer : HeapBuffer
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000077A1 File Offset: 0x000059A1
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000077A4 File Offset: 0x000059A4
		public TableHeapBuffer(ModuleDefinition module, MetadataBuilder metadata) : base(24)
		{
			this.module = module;
			this.metadata = metadata;
			this.counter = new Func<Table, int>(this.GetTableLength);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000077F4 File Offset: 0x000059F4
		private int GetTableLength(Table table)
		{
			MetadataTable metadataTable = this.tables[(int)table];
			if (metadataTable == null)
			{
				return 0;
			}
			return metadataTable.Length;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007818 File Offset: 0x00005A18
		public TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			TTable ttable = (TTable)((object)this.tables[(int)table]);
			if (ttable != null)
			{
				return ttable;
			}
			ttable = Activator.CreateInstance<TTable>();
			this.tables[(int)table] = ttable;
			return ttable;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007852 File Offset: 0x00005A52
		public void WriteBySize(uint value, int size)
		{
			if (size == 4)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007868 File Offset: 0x00005A68
		public void WriteBySize(uint value, bool large)
		{
			if (large)
			{
				base.WriteUInt32(value);
				return;
			}
			base.WriteUInt16((ushort)value);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000787D File Offset: 0x00005A7D
		public void WriteString(uint @string)
		{
			this.WriteBySize(@string, this.large_string);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000788C File Offset: 0x00005A8C
		public void WriteBlob(uint blob)
		{
			this.WriteBySize(blob, this.large_blob);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000789C File Offset: 0x00005A9C
		public void WriteRID(uint rid, Table table)
		{
			MetadataTable metadataTable = this.tables[(int)table];
			this.WriteBySize(rid, metadataTable != null && metadataTable.IsLarge);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000078C8 File Offset: 0x00005AC8
		private int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = this.coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return this.coded_index_sizes[(int)coded_index] = coded_index.GetSize(this.counter);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000078FC File Offset: 0x00005AFC
		public void WriteCodedRID(uint rid, CodedIndex coded_index)
		{
			this.WriteBySize(rid, this.GetCodedIndexSize(coded_index));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000790C File Offset: 0x00005B0C
		public void WriteTableHeap()
		{
			base.WriteUInt32(0U);
			base.WriteByte(this.GetTableHeapVersion());
			base.WriteByte(0);
			base.WriteByte(this.GetHeapSizes());
			base.WriteByte(10);
			base.WriteUInt64(this.GetValid());
			base.WriteUInt64(24190111578624UL);
			this.WriteRowCount();
			this.WriteTables();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007970 File Offset: 0x00005B70
		private void WriteRowCount()
		{
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					base.WriteUInt32((uint)metadataTable.Length);
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000079B0 File Offset: 0x00005BB0
		private void WriteTables()
		{
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Write(this);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000079EC File Offset: 0x00005BEC
		private ulong GetValid()
		{
			ulong num = 0UL;
			for (int i = 0; i < this.tables.Length; i++)
			{
				MetadataTable metadataTable = this.tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Sort();
					num |= 1UL << i;
				}
			}
			return num;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007A34 File Offset: 0x00005C34
		private byte GetHeapSizes()
		{
			byte b = 0;
			if (this.metadata.string_heap.IsLarge)
			{
				this.large_string = true;
				b |= 1;
			}
			if (this.metadata.blob_heap.IsLarge)
			{
				this.large_blob = true;
				b |= 4;
			}
			return b;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007A80 File Offset: 0x00005C80
		private byte GetTableHeapVersion()
		{
			switch (this.module.Runtime)
			{
			case TargetRuntime.Net_1_0:
			case TargetRuntime.Net_1_1:
				return 1;
			default:
				return 2;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007AAC File Offset: 0x00005CAC
		public void FixupData(uint data_rva)
		{
			FieldRVATable table = this.GetTable<FieldRVATable>(Table.FieldRVA);
			if (table.length == 0)
			{
				return;
			}
			int num = this.GetTable<FieldTable>(Table.Field).IsLarge ? 4 : 2;
			int position = this.position;
			this.position = table.position;
			for (int i = 0; i < table.length; i++)
			{
				uint num2 = base.ReadUInt32();
				this.position -= 4;
				base.WriteUInt32(num2 + data_rva);
				this.position += num;
			}
			this.position = position;
		}

		// Token: 0x0400027B RID: 635
		private readonly ModuleDefinition module;

		// Token: 0x0400027C RID: 636
		private readonly MetadataBuilder metadata;

		// Token: 0x0400027D RID: 637
		internal MetadataTable[] tables = new MetadataTable[45];

		// Token: 0x0400027E RID: 638
		private bool large_string;

		// Token: 0x0400027F RID: 639
		private bool large_blob;

		// Token: 0x04000280 RID: 640
		private readonly int[] coded_index_sizes = new int[13];

		// Token: 0x04000281 RID: 641
		private readonly Func<Table, int> counter;
	}
}
