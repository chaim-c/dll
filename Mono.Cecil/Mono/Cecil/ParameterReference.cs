using System;

namespace Mono.Cecil
{
	// Token: 0x020000D2 RID: 210
	public abstract class ParameterReference : IMetadataTokenProvider
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001AD9C File Offset: 0x00018F9C
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public string Name
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001ADAD File Offset: 0x00018FAD
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001ADB5 File Offset: 0x00018FB5
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0001ADBD File Offset: 0x00018FBD
		public TypeReference ParameterType
		{
			get
			{
				return this.parameter_type;
			}
			set
			{
				this.parameter_type = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001ADC6 File Offset: 0x00018FC6
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001ADCE File Offset: 0x00018FCE
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

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001ADD7 File Offset: 0x00018FD7
		internal ParameterReference(string name, TypeReference parameterType)
		{
			if (parameterType == null)
			{
				throw new ArgumentNullException("parameterType");
			}
			this.name = (name ?? string.Empty);
			this.parameter_type = parameterType;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001AE0B File Offset: 0x0001900B
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060007F7 RID: 2039
		public abstract ParameterDefinition Resolve();

		// Token: 0x0400051C RID: 1308
		private string name;

		// Token: 0x0400051D RID: 1309
		internal int index = -1;

		// Token: 0x0400051E RID: 1310
		protected TypeReference parameter_type;

		// Token: 0x0400051F RID: 1311
		internal MetadataToken token;
	}
}
