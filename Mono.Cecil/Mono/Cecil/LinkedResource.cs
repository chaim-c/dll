using System;

namespace Mono.Cecil
{
	// Token: 0x020000CA RID: 202
	public sealed class LinkedResource : Resource
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001A210 File Offset: 0x00018410
		public byte[] Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001A218 File Offset: 0x00018418
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0001A220 File Offset: 0x00018420
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001A229 File Offset: 0x00018429
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Linked;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001A22C File Offset: 0x0001842C
		public LinkedResource(string name, ManifestResourceAttributes flags) : base(name, flags)
		{
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001A236 File Offset: 0x00018436
		public LinkedResource(string name, ManifestResourceAttributes flags, string file) : base(name, flags)
		{
			this.file = file;
		}

		// Token: 0x040004C7 RID: 1223
		internal byte[] hash;

		// Token: 0x040004C8 RID: 1224
		private string file;
	}
}
