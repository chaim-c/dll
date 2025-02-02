using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000D3 RID: 211
	public sealed class ParameterDefinition : ParameterReference, ICustomAttributeProvider, IConstantProvider, IMarshalInfoProvider, IMetadataTokenProvider
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001AE13 File Offset: 0x00019013
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001AE1B File Offset: 0x0001901B
		public ParameterAttributes Attributes
		{
			get
			{
				return (ParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001AE24 File Offset: 0x00019024
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001AE2C File Offset: 0x0001902C
		public int Sequence
		{
			get
			{
				if (this.method == null)
				{
					return -1;
				}
				if (!this.method.HasImplicitThis())
				{
					return this.index;
				}
				return this.index + 1;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001AE54 File Offset: 0x00019054
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0001AE7D File Offset: 0x0001907D
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.parameter_type.Module);
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001AE8D File Offset: 0x0001908D
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0001AE9F File Offset: 0x0001909F
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001AEA8 File Offset: 0x000190A8
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.parameter_type.Module);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001AED2 File Offset: 0x000190D2
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.parameter_type.Module);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001AEF5 File Offset: 0x000190F5
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.parameter_type.Module);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001AF12 File Offset: 0x00019112
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001AF35 File Offset: 0x00019135
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.parameter_type.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001AF3E File Offset: 0x0001913E
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x0001AF4C File Offset: 0x0001914C
		public bool IsIn
		{
			get
			{
				return this.attributes.GetAttributes(1);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1, value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001AF61 File Offset: 0x00019161
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0001AF6F File Offset: 0x0001916F
		public bool IsOut
		{
			get
			{
				return this.attributes.GetAttributes(2);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2, value);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001AF84 File Offset: 0x00019184
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001AF92 File Offset: 0x00019192
		public bool IsLcid
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

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001AFA7 File Offset: 0x000191A7
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001AFB5 File Offset: 0x000191B5
		public bool IsReturnValue
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001AFCA File Offset: 0x000191CA
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001AFD9 File Offset: 0x000191D9
		public bool IsOptional
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

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001AFEF File Offset: 0x000191EF
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001B001 File Offset: 0x00019201
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001B01A File Offset: 0x0001921A
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001B02C File Offset: 0x0001922C
		public bool HasFieldMarshal
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

		// Token: 0x06000813 RID: 2067 RVA: 0x0001B045 File Offset: 0x00019245
		internal ParameterDefinition(TypeReference parameterType, IMethodSignature method) : this(string.Empty, ParameterAttributes.None, parameterType)
		{
			this.method = method;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001B05B File Offset: 0x0001925B
		public ParameterDefinition(TypeReference parameterType) : this(string.Empty, ParameterAttributes.None, parameterType)
		{
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B06A File Offset: 0x0001926A
		public ParameterDefinition(string name, ParameterAttributes attributes, TypeReference parameterType) : base(name, parameterType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Param);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001B096 File Offset: 0x00019296
		public override ParameterDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000520 RID: 1312
		private ushort attributes;

		// Token: 0x04000521 RID: 1313
		internal IMethodSignature method;

		// Token: 0x04000522 RID: 1314
		private object constant = Mixin.NotResolved;

		// Token: 0x04000523 RID: 1315
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000524 RID: 1316
		private MarshalInfo marshal_info;
	}
}
