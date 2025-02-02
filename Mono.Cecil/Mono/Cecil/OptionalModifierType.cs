using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000BB RID: 187
	public sealed class OptionalModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00018EAD File Offset: 0x000170AD
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00018EB5 File Offset: 0x000170B5
		public TypeReference ModifierType
		{
			get
			{
				return this.modifier_type;
			}
			set
			{
				this.modifier_type = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00018EBE File Offset: 0x000170BE
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00018ED1 File Offset: 0x000170D1
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00018EE4 File Offset: 0x000170E4
		private string Suffix
		{
			get
			{
				return " modopt(" + this.modifier_type + ")";
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00018EFB File Offset: 0x000170FB
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00018EFE File Offset: 0x000170FE
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00018F05 File Offset: 0x00017105
		public override bool IsOptionalModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00018F08 File Offset: 0x00017108
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018F1F File Offset: 0x0001711F
		public OptionalModifierType(TypeReference modifierType, TypeReference type) : base(type)
		{
			Mixin.CheckModifier(modifierType, type);
			this.modifier_type = modifierType;
			this.etype = Mono.Cecil.Metadata.ElementType.CModOpt;
		}

		// Token: 0x04000456 RID: 1110
		private TypeReference modifier_type;
	}
}
