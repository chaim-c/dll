using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000CF RID: 207
	public sealed class MethodReturnType : IConstantProvider, ICustomAttributeProvider, IMarshalInfoProvider, IMetadataTokenProvider
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001AC30 File Offset: 0x00018E30
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001AC38 File Offset: 0x00018E38
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0001AC40 File Offset: 0x00018E40
		public TypeReference ReturnType
		{
			get
			{
				return this.return_type;
			}
			set
			{
				this.return_type = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001AC49 File Offset: 0x00018E49
		internal ParameterDefinition Parameter
		{
			get
			{
				if (this.parameter == null)
				{
					Interlocked.CompareExchange<ParameterDefinition>(ref this.parameter, new ParameterDefinition(this.return_type, this.method), null);
				}
				return this.parameter;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001AC77 File Offset: 0x00018E77
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001AC84 File Offset: 0x00018E84
		public MetadataToken MetadataToken
		{
			get
			{
				return this.Parameter.MetadataToken;
			}
			set
			{
				this.Parameter.MetadataToken = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001AC92 File Offset: 0x00018E92
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001AC9F File Offset: 0x00018E9F
		public ParameterAttributes Attributes
		{
			get
			{
				return this.Parameter.Attributes;
			}
			set
			{
				this.Parameter.Attributes = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001ACAD File Offset: 0x00018EAD
		public bool HasCustomAttributes
		{
			get
			{
				return this.parameter != null && this.parameter.HasCustomAttributes;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.Parameter.CustomAttributes;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001ACD1 File Offset: 0x00018ED1
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		public bool HasDefault
		{
			get
			{
				return this.parameter != null && this.parameter.HasDefault;
			}
			set
			{
				this.Parameter.HasDefault = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001ACF6 File Offset: 0x00018EF6
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001AD0D File Offset: 0x00018F0D
		public bool HasConstant
		{
			get
			{
				return this.parameter != null && this.parameter.HasConstant;
			}
			set
			{
				this.Parameter.HasConstant = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001AD1B File Offset: 0x00018F1B
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001AD28 File Offset: 0x00018F28
		public object Constant
		{
			get
			{
				return this.Parameter.Constant;
			}
			set
			{
				this.Parameter.Constant = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001AD36 File Offset: 0x00018F36
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001AD4D File Offset: 0x00018F4D
		public bool HasFieldMarshal
		{
			get
			{
				return this.parameter != null && this.parameter.HasFieldMarshal;
			}
			set
			{
				this.Parameter.HasFieldMarshal = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001AD5B File Offset: 0x00018F5B
		public bool HasMarshalInfo
		{
			get
			{
				return this.parameter != null && this.parameter.HasMarshalInfo;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001AD72 File Offset: 0x00018F72
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0001AD7F File Offset: 0x00018F7F
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.Parameter.MarshalInfo;
			}
			set
			{
				this.Parameter.MarshalInfo = value;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001AD8D File Offset: 0x00018F8D
		public MethodReturnType(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x04000507 RID: 1287
		internal IMethodSignature method;

		// Token: 0x04000508 RID: 1288
		internal ParameterDefinition parameter;

		// Token: 0x04000509 RID: 1289
		private TypeReference return_type;
	}
}
