using System;

namespace Mono.Cecil
{
	// Token: 0x02000054 RID: 84
	public abstract class TypeSpecification : TypeReference
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BF70 File Offset: 0x0000A170
		public TypeReference ElementType
		{
			get
			{
				return this.element_type;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BF78 File Offset: 0x0000A178
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000BF85 File Offset: 0x0000A185
		public override string Name
		{
			get
			{
				return this.element_type.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000BF8C File Offset: 0x0000A18C
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000BF99 File Offset: 0x0000A199
		public override string Namespace
		{
			get
			{
				return this.element_type.Namespace;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000BFAD File Offset: 0x0000A1AD
		public override IMetadataScope Scope
		{
			get
			{
				return this.element_type.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public override ModuleDefinition Module
		{
			get
			{
				return this.element_type.Module;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000BFC1 File Offset: 0x0000A1C1
		public override string FullName
		{
			get
			{
				return this.element_type.FullName;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BFCE File Offset: 0x0000A1CE
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.element_type.ContainsGenericParameter;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000BFDB File Offset: 0x0000A1DB
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BFE3 File Offset: 0x0000A1E3
		internal TypeSpecification(TypeReference type) : base(null, null)
		{
			this.element_type = type;
			this.token = new MetadataToken(TokenType.TypeSpec);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C004 File Offset: 0x0000A204
		public override TypeReference GetElementType()
		{
			return this.element_type.GetElementType();
		}

		// Token: 0x04000382 RID: 898
		private readonly TypeReference element_type;
	}
}
