using System;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200001C RID: 28
	public struct FolderId : IEquatable<FolderId>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003F43 File Offset: 0x00002143
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003F4B File Offset: 0x0000214B
		public int LocalId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003F54 File Offset: 0x00002154
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003F5C File Offset: 0x0000215C
		public SaveFolderExtension Extension { get; private set; }

		// Token: 0x0600008D RID: 141 RVA: 0x00003F65 File Offset: 0x00002165
		public FolderId(int localId, SaveFolderExtension extension)
		{
			this.LocalId = localId;
			this.Extension = extension;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F78 File Offset: 0x00002178
		public override bool Equals(object obj)
		{
			if (!(obj is FolderId))
			{
				return false;
			}
			FolderId folderId = (FolderId)obj;
			return folderId.LocalId == this.LocalId && folderId.Extension == this.Extension;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FB6 File Offset: 0x000021B6
		public bool Equals(FolderId other)
		{
			return other.LocalId == this.LocalId && other.Extension == this.Extension;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003FD8 File Offset: 0x000021D8
		public override int GetHashCode()
		{
			return this.LocalId.GetHashCode() * 397 ^ ((int)this.Extension).GetHashCode();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004008 File Offset: 0x00002208
		public static bool operator ==(FolderId a, FolderId b)
		{
			return a.LocalId == b.LocalId && a.Extension == b.Extension;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000402C File Offset: 0x0000222C
		public static bool operator !=(FolderId a, FolderId b)
		{
			return !(a == b);
		}
	}
}
