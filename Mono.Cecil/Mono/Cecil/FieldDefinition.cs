using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000C9 RID: 201
	public sealed class FieldDefinition : FieldReference, IMemberDefinition, ICustomAttributeProvider, IConstantProvider, IMarshalInfoProvider, IMetadataTokenProvider
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00019D30 File Offset: 0x00017F30
		private void ResolveLayout()
		{
			if (this.offset != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				this.offset = -1;
				return;
			}
			this.offset = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldLayout(field));
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00019D87 File Offset: 0x00017F87
		public bool HasLayoutInfo
		{
			get
			{
				if (this.offset >= 0)
				{
					return true;
				}
				this.ResolveLayout();
				return this.offset >= 0;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00019DA6 File Offset: 0x00017FA6
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00019DCF File Offset: 0x00017FCF
		public int Offset
		{
			get
			{
				if (this.offset >= 0)
				{
					return this.offset;
				}
				this.ResolveLayout();
				if (this.offset < 0)
				{
					return -1;
				}
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00019DE4 File Offset: 0x00017FE4
		private void ResolveRVA()
		{
			if (this.rva != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				return;
			}
			this.rva = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldRVA(field));
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00019E34 File Offset: 0x00018034
		public int RVA
		{
			get
			{
				if (this.rva > 0)
				{
					return this.rva;
				}
				this.ResolveRVA();
				if (this.rva <= 0)
				{
					return 0;
				}
				return this.rva;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00019E5D File Offset: 0x0001805D
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00019E8D File Offset: 0x0001808D
		public byte[] InitialValue
		{
			get
			{
				if (this.initial_value != null)
				{
					return this.initial_value;
				}
				this.ResolveRVA();
				if (this.initial_value == null)
				{
					this.initial_value = Empty<byte>.Array;
				}
				return this.initial_value;
			}
			set
			{
				this.initial_value = value;
				this.rva = 0;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00019E9D File Offset: 0x0001809D
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00019EA5 File Offset: 0x000180A5
		public FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00019EAE File Offset: 0x000180AE
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00019ED2 File Offset: 0x000180D2
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00019EE2 File Offset: 0x000180E2
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00019EF4 File Offset: 0x000180F4
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00019EFD File Offset: 0x000180FD
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

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00019F22 File Offset: 0x00018122
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00019F40 File Offset: 0x00018140
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.Module);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00019F58 File Offset: 0x00018158
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00019F76 File Offset: 0x00018176
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00019F7F File Offset: 0x0001817F
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00019F8E File Offset: 0x0001818E
		public bool IsCompilerControlled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 0U, value);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00019FA4 File Offset: 0x000181A4
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00019FB3 File Offset: 0x000181B3
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 1U, value);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00019FC9 File Offset: 0x000181C9
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00019FD8 File Offset: 0x000181D8
		public bool IsFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 2U, value);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00019FEE File Offset: 0x000181EE
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00019FFD File Offset: 0x000181FD
		public bool IsAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 3U, value);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001A013 File Offset: 0x00018213
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001A022 File Offset: 0x00018222
		public bool IsFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 4U, value);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001A038 File Offset: 0x00018238
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001A047 File Offset: 0x00018247
		public bool IsFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 5U, value);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001A05D File Offset: 0x0001825D
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001A06C File Offset: 0x0001826C
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 6U, value);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001A082 File Offset: 0x00018282
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001A091 File Offset: 0x00018291
		public bool IsStatic
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001A0A7 File Offset: 0x000182A7
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001A0B6 File Offset: 0x000182B6
		public bool IsInitOnly
		{
			get
			{
				return this.attributes.GetAttributes(32);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001A0CC File Offset: 0x000182CC
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001A0DB File Offset: 0x000182DB
		public bool IsLiteral
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001A0F1 File Offset: 0x000182F1
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001A103 File Offset: 0x00018303
		public bool IsNotSerialized
		{
			get
			{
				return this.attributes.GetAttributes(128);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001A11C File Offset: 0x0001831C
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001A12E File Offset: 0x0001832E
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001A147 File Offset: 0x00018347
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001A159 File Offset: 0x00018359
		public bool IsPInvokeImpl
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001A172 File Offset: 0x00018372
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001A184 File Offset: 0x00018384
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001A19D File Offset: 0x0001839D
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001A1AF File Offset: 0x000183AF
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(32768);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32768, value);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001A1C8 File Offset: 0x000183C8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001A1CB File Offset: 0x000183CB
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0001A1D8 File Offset: 0x000183D8
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001A1E1 File Offset: 0x000183E1
		public FieldDefinition(string name, FieldAttributes attributes, TypeReference fieldType) : base(name, fieldType)
		{
			this.attributes = (ushort)attributes;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001A20D File Offset: 0x0001840D
		public override FieldDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040004BE RID: 1214
		private ushort attributes;

		// Token: 0x040004BF RID: 1215
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040004C0 RID: 1216
		private int offset = -2;

		// Token: 0x040004C1 RID: 1217
		internal int rva = -2;

		// Token: 0x040004C2 RID: 1218
		private byte[] initial_value;

		// Token: 0x040004C3 RID: 1219
		private object constant = Mixin.NotResolved;

		// Token: 0x040004C4 RID: 1220
		private MarshalInfo marshal_info;
	}
}
