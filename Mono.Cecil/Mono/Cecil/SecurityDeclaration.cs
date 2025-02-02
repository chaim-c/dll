using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200009F RID: 159
	public sealed class SecurityDeclaration
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00017704 File Offset: 0x00015904
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001770C File Offset: 0x0001590C
		public SecurityAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00017715 File Offset: 0x00015915
		public bool HasSecurityAttributes
		{
			get
			{
				this.Resolve();
				return !this.security_attributes.IsNullOrEmpty<SecurityAttribute>();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001772C File Offset: 0x0001592C
		public Collection<SecurityAttribute> SecurityAttributes
		{
			get
			{
				this.Resolve();
				Collection<SecurityAttribute> result;
				if ((result = this.security_attributes) == null)
				{
					result = (this.security_attributes = new Collection<SecurityAttribute>());
				}
				return result;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00017757 File Offset: 0x00015957
		internal bool HasImage
		{
			get
			{
				return this.module != null && this.module.HasImage;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001776E File Offset: 0x0001596E
		internal SecurityDeclaration(SecurityAction action, uint signature, ModuleDefinition module)
		{
			this.action = action;
			this.signature = signature;
			this.module = module;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001778B File Offset: 0x0001598B
		public SecurityDeclaration(SecurityAction action)
		{
			this.action = action;
			this.resolved = true;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000177A1 File Offset: 0x000159A1
		public SecurityDeclaration(SecurityAction action, byte[] blob)
		{
			this.action = action;
			this.resolved = false;
			this.blob = blob;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000177CC File Offset: 0x000159CC
		public byte[] GetBlob()
		{
			if (this.blob != null)
			{
				return this.blob;
			}
			if (!this.HasImage || this.signature == 0U)
			{
				throw new NotSupportedException();
			}
			return this.blob = this.module.Read<SecurityDeclaration, byte[]>(this, (SecurityDeclaration declaration, MetadataReader reader) => reader.ReadSecurityDeclarationBlob(declaration.signature));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001783A File Offset: 0x00015A3A
		private void Resolve()
		{
			if (this.resolved || !this.HasImage)
			{
				return;
			}
			this.module.Read<SecurityDeclaration, SecurityDeclaration>(this, delegate(SecurityDeclaration declaration, MetadataReader reader)
			{
				reader.ReadSecurityDeclarationSignature(declaration);
				return this;
			});
			this.resolved = true;
		}

		// Token: 0x04000411 RID: 1041
		internal readonly uint signature;

		// Token: 0x04000412 RID: 1042
		private byte[] blob;

		// Token: 0x04000413 RID: 1043
		private readonly ModuleDefinition module;

		// Token: 0x04000414 RID: 1044
		internal bool resolved;

		// Token: 0x04000415 RID: 1045
		private SecurityAction action;

		// Token: 0x04000416 RID: 1046
		internal Collection<SecurityAttribute> security_attributes;
	}
}
