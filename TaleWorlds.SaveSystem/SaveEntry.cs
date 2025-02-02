using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200001F RID: 31
	public class SaveEntry
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004245 File Offset: 0x00002445
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000424D File Offset: 0x0000244D
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004255 File Offset: 0x00002455
		public EntryId Id { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000425E File Offset: 0x0000245E
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004266 File Offset: 0x00002466
		public int FolderId { get; private set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x00004277 File Offset: 0x00002477
		public static SaveEntry CreateFrom(int entryFolderId, EntryId entryId, byte[] data)
		{
			return new SaveEntry
			{
				FolderId = entryFolderId,
				Id = entryId,
				_data = data
			};
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004293 File Offset: 0x00002493
		public static SaveEntry CreateNew(SaveEntryFolder parentFolder, EntryId entryId)
		{
			return new SaveEntry
			{
				Id = entryId,
				FolderId = parentFolder.GlobalId
			};
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000042AD File Offset: 0x000024AD
		public BinaryReader GetBinaryReader()
		{
			return new BinaryReader(this._data);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000042BA File Offset: 0x000024BA
		public void FillFrom(BinaryWriter writer)
		{
			this._data = writer.Data;
		}

		// Token: 0x04000042 RID: 66
		private byte[] _data;
	}
}
