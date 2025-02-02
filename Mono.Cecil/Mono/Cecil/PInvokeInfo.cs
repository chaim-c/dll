using System;

namespace Mono.Cecil
{
	// Token: 0x020000D5 RID: 213
	public sealed class PInvokeInfo
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001B099 File Offset: 0x00019299
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0001B0A1 File Offset: 0x000192A1
		public PInvokeAttributes Attributes
		{
			get
			{
				return (PInvokeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001B0AA File Offset: 0x000192AA
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0001B0B2 File Offset: 0x000192B2
		public string EntryPoint
		{
			get
			{
				return this.entry_point;
			}
			set
			{
				this.entry_point = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001B0BB File Offset: 0x000192BB
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0001B0C3 File Offset: 0x000192C3
		public ModuleReference Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001B0CC File Offset: 0x000192CC
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001B0DA File Offset: 0x000192DA
		public bool IsNoMangle
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001B0EF File Offset: 0x000192EF
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001B0FE File Offset: 0x000192FE
		public bool IsCharSetNotSpec
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 0U, value);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001B114 File Offset: 0x00019314
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001B123 File Offset: 0x00019323
		public bool IsCharSetAnsi
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 2U, value);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001B139 File Offset: 0x00019339
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001B148 File Offset: 0x00019348
		public bool IsCharSetUnicode
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 4U, value);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001B15E File Offset: 0x0001935E
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001B16D File Offset: 0x0001936D
		public bool IsCharSetAuto
		{
			get
			{
				return this.attributes.GetMaskedAttributes(6, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(6, 6U, value);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001B183 File Offset: 0x00019383
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001B192 File Offset: 0x00019392
		public bool SupportsLastError
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001B1A8 File Offset: 0x000193A8
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0001B1BF File Offset: 0x000193BF
		public bool IsCallConvWinapi
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 256U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 256U, value);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001B1DD File Offset: 0x000193DD
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x0001B1F4 File Offset: 0x000193F4
		public bool IsCallConvCdecl
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 512U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 512U, value);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001B212 File Offset: 0x00019412
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001B229 File Offset: 0x00019429
		public bool IsCallConvStdCall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 768U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 768U, value);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001B247 File Offset: 0x00019447
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0001B25E File Offset: 0x0001945E
		public bool IsCallConvThiscall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 1024U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 1024U, value);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001B27C File Offset: 0x0001947C
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0001B293 File Offset: 0x00019493
		public bool IsCallConvFastcall
		{
			get
			{
				return this.attributes.GetMaskedAttributes(1792, 1280U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(1792, 1280U, value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001B2B1 File Offset: 0x000194B1
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x0001B2C2 File Offset: 0x000194C2
		public bool IsBestFitEnabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(48, 16U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(48, 16U, value);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001B2DA File Offset: 0x000194DA
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001B2EB File Offset: 0x000194EB
		public bool IsBestFitDisabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(48, 32U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(48, 32U, value);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001B303 File Offset: 0x00019503
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0001B31A File Offset: 0x0001951A
		public bool IsThrowOnUnmappableCharEnabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(12288, 4096U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(12288, 4096U, value);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001B338 File Offset: 0x00019538
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001B34F File Offset: 0x0001954F
		public bool IsThrowOnUnmappableCharDisabled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(12288, 8192U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(12288, 8192U, value);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B36D File Offset: 0x0001956D
		public PInvokeInfo(PInvokeAttributes attributes, string entryPoint, ModuleReference module)
		{
			this.attributes = (ushort)attributes;
			this.entry_point = entryPoint;
			this.module = module;
		}

		// Token: 0x04000539 RID: 1337
		private ushort attributes;

		// Token: 0x0400053A RID: 1338
		private string entry_point;

		// Token: 0x0400053B RID: 1339
		private ModuleReference module;
	}
}
