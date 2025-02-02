using System;

namespace Mono.Cecil
{
	// Token: 0x02000050 RID: 80
	public abstract class MemberReference : IMetadataTokenProvider
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000BC40 File Offset: 0x00009E40
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000BC48 File Offset: 0x00009E48
		public virtual string Name
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000282 RID: 642
		public abstract string FullName { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000BC51 File Offset: 0x00009E51
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000BC59 File Offset: 0x00009E59
		public virtual TypeReference DeclaringType
		{
			get
			{
				return this.declaring_type;
			}
			set
			{
				this.declaring_type = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000BC62 File Offset: 0x00009E62
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000BC6A File Offset: 0x00009E6A
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000BC74 File Offset: 0x00009E74
		internal bool HasImage
		{
			get
			{
				ModuleDefinition module = this.Module;
				return module != null && module.HasImage;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000BC93 File Offset: 0x00009E93
		public virtual ModuleDefinition Module
		{
			get
			{
				if (this.declaring_type == null)
				{
					return null;
				}
				return this.declaring_type.Module;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000BCAA File Offset: 0x00009EAA
		public virtual bool IsDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000BCAD File Offset: 0x00009EAD
		public virtual bool ContainsGenericParameter
		{
			get
			{
				return this.declaring_type != null && this.declaring_type.ContainsGenericParameter;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		internal MemberReference()
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BCCC File Offset: 0x00009ECC
		internal MemberReference(string name)
		{
			this.name = (name ?? string.Empty);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		internal string MemberFullName()
		{
			if (this.declaring_type == null)
			{
				return this.name;
			}
			return this.declaring_type.FullName + "::" + this.name;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BD10 File Offset: 0x00009F10
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000378 RID: 888
		private string name;

		// Token: 0x04000379 RID: 889
		private TypeReference declaring_type;

		// Token: 0x0400037A RID: 890
		internal MetadataToken token;
	}
}
