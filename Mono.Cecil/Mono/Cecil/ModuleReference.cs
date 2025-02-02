using System;

namespace Mono.Cecil
{
	// Token: 0x020000DD RID: 221
	public class ModuleReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001B8C7 File Offset: 0x00019AC7
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0001B8CF File Offset: 0x00019ACF
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.ModuleReference;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001B8DB File Offset: 0x00019ADB
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001B8E3 File Offset: 0x00019AE3
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001B8EC File Offset: 0x00019AEC
		internal ModuleReference()
		{
			this.token = new MetadataToken(TokenType.ModuleRef);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001B904 File Offset: 0x00019B04
		public ModuleReference(string name) : this()
		{
			this.name = name;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001B913 File Offset: 0x00019B13
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04000553 RID: 1363
		private string name;

		// Token: 0x04000554 RID: 1364
		internal MetadataToken token;
	}
}
