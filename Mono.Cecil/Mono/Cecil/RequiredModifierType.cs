using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020000BC RID: 188
	public sealed class RequiredModifierType : TypeSpecification, IModifierType
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00018F3E File Offset: 0x0001713E
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00018F46 File Offset: 0x00017146
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00018F4F File Offset: 0x0001714F
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00018F62 File Offset: 0x00017162
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00018F75 File Offset: 0x00017175
		private string Suffix
		{
			get
			{
				return " modreq(" + this.modifier_type + ")";
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00018F8C File Offset: 0x0001718C
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00018F8F File Offset: 0x0001718F
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00018F96 File Offset: 0x00017196
		public override bool IsRequiredModifier
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00018F99 File Offset: 0x00017199
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018FB0 File Offset: 0x000171B0
		public RequiredModifierType(TypeReference modifierType, TypeReference type) : base(type)
		{
			Mixin.CheckModifier(modifierType, type);
			this.modifier_type = modifierType;
			this.etype = Mono.Cecil.Metadata.ElementType.CModReqD;
		}

		// Token: 0x04000457 RID: 1111
		private TypeReference modifier_type;
	}
}
