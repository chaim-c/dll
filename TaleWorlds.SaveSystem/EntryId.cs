using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200001D RID: 29
	public struct EntryId : IEquatable<EntryId>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004038 File Offset: 0x00002238
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004040 File Offset: 0x00002240
		public int Id { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004049 File Offset: 0x00002249
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004051 File Offset: 0x00002251
		public SaveEntryExtension Extension { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x0000405A File Offset: 0x0000225A
		public EntryId(int id, SaveEntryExtension extension)
		{
			this.Id = id;
			this.Extension = extension;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000406C File Offset: 0x0000226C
		public override bool Equals(object obj)
		{
			if (!(obj is EntryId))
			{
				return false;
			}
			EntryId entryId = (EntryId)obj;
			return entryId.Id == this.Id && entryId.Extension == this.Extension;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000040AA File Offset: 0x000022AA
		public bool Equals(EntryId other)
		{
			return other.Id == this.Id && other.Extension == this.Extension;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000040CC File Offset: 0x000022CC
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() * 397 ^ ((int)this.Extension).GetHashCode();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000040FC File Offset: 0x000022FC
		public static bool operator ==(EntryId a, EntryId b)
		{
			return a.Id == b.Id && a.Extension == b.Extension;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004120 File Offset: 0x00002320
		public static bool operator !=(EntryId a, EntryId b)
		{
			return !(a == b);
		}
	}
}
