using System;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000AB RID: 171
	public sealed class GenericParameter : TypeReference, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001823B File Offset: 0x0001643B
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00018243 File Offset: 0x00016443
		public GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001824C File Offset: 0x0001644C
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00018254 File Offset: 0x00016454
		public GenericParameterType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001825C File Offset: 0x0001645C
		public IGenericParameterProvider Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00018270 File Offset: 0x00016470
		public bool HasConstraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, bool>(this, (GenericParameter generic_parameter, MetadataReader reader) => reader.HasGenericConstraints(generic_parameter));
				}
				return false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000182D0 File Offset: 0x000164D0
		public Collection<TypeReference> Constraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, Collection<TypeReference>>(ref this.constraints, this, (GenericParameter generic_parameter, MetadataReader reader) => reader.ReadGenericConstraints(generic_parameter));
				}
				return this.constraints = new Collection<TypeReference>();
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00018332 File Offset: 0x00016532
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00018357 File Offset: 0x00016557
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00018375 File Offset: 0x00016575
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x000183B5 File Offset: 0x000165B5
		public override IMetadataScope Scope
		{
			get
			{
				if (this.owner == null)
				{
					return null;
				}
				if (this.owner.GenericParameterType != GenericParameterType.Method)
				{
					return ((TypeReference)this.owner).Scope;
				}
				return ((MethodReference)this.owner).DeclaringType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000183BC File Offset: 0x000165BC
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000183C9 File Offset: 0x000165C9
		public override TypeReference DeclaringType
		{
			get
			{
				return this.owner as TypeReference;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000183D0 File Offset: 0x000165D0
		public MethodReference DeclaringMethod
		{
			get
			{
				return this.owner as MethodReference;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000183DD File Offset: 0x000165DD
		public override ModuleDefinition Module
		{
			get
			{
				return this.module ?? this.owner.Module;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000183F4 File Offset: 0x000165F4
		public override string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Name))
				{
					return base.Name;
				}
				return base.Name = ((this.type == GenericParameterType.Method) ? "!!" : "!") + this.position;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00018443 File Offset: 0x00016643
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001844A File Offset: 0x0001664A
		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00018451 File Offset: 0x00016651
		public override string FullName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00018459 File Offset: 0x00016659
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001845C File Offset: 0x0001665C
		public override bool ContainsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001845F File Offset: 0x0001665F
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00018467 File Offset: 0x00016667
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00018476 File Offset: 0x00016676
		public bool IsNonVariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 0U, value);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001848C File Offset: 0x0001668C
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0001849B File Offset: 0x0001669B
		public bool IsCovariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 1U, value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000184B1 File Offset: 0x000166B1
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000184C0 File Offset: 0x000166C0
		public bool IsContravariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 2U, value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000184D6 File Offset: 0x000166D6
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000184E4 File Offset: 0x000166E4
		public bool HasReferenceTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(4);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4, value);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000184F9 File Offset: 0x000166F9
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00018507 File Offset: 0x00016707
		public bool HasNotNullableValueTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001851C File Offset: 0x0001671C
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001852B File Offset: 0x0001672B
		public bool HasDefaultConstructorConstraint
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00018541 File Offset: 0x00016741
		public GenericParameter(IGenericParameterProvider owner) : this(string.Empty, owner)
		{
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00018550 File Offset: 0x00016750
		public GenericParameter(string name, IGenericParameterProvider owner) : base(string.Empty, name)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.position = -1;
			this.owner = owner;
			this.type = owner.GenericParameterType;
			this.etype = GenericParameter.ConvertGenericParameterType(this.type);
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000185B0 File Offset: 0x000167B0
		internal GenericParameter(int position, GenericParameterType type, ModuleDefinition module) : base(string.Empty, string.Empty)
		{
			if (module == null)
			{
				throw new ArgumentNullException();
			}
			this.position = position;
			this.type = type;
			this.etype = GenericParameter.ConvertGenericParameterType(type);
			this.module = module;
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00018608 File Offset: 0x00016808
		private static ElementType ConvertGenericParameterType(GenericParameterType type)
		{
			switch (type)
			{
			case GenericParameterType.Type:
				return ElementType.Var;
			case GenericParameterType.Method:
				return ElementType.MVar;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00018632 File Offset: 0x00016832
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x04000432 RID: 1074
		internal int position;

		// Token: 0x04000433 RID: 1075
		internal GenericParameterType type;

		// Token: 0x04000434 RID: 1076
		internal IGenericParameterProvider owner;

		// Token: 0x04000435 RID: 1077
		private ushort attributes;

		// Token: 0x04000436 RID: 1078
		private Collection<TypeReference> constraints;

		// Token: 0x04000437 RID: 1079
		private Collection<CustomAttribute> custom_attributes;
	}
}
