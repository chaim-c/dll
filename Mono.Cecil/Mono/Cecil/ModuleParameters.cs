using System;

namespace Mono.Cecil
{
	// Token: 0x020000E1 RID: 225
	public sealed class ModuleParameters
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001BA8E File Offset: 0x00019C8E
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001BA96 File Offset: 0x00019C96
		public ModuleKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001BA9F File Offset: 0x00019C9F
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001BAA7 File Offset: 0x00019CA7
		public TargetRuntime Runtime
		{
			get
			{
				return this.runtime;
			}
			set
			{
				this.runtime = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		public TargetArchitecture Architecture
		{
			get
			{
				return this.architecture;
			}
			set
			{
				this.architecture = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001BAC1 File Offset: 0x00019CC1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001BAC9 File Offset: 0x00019CC9
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
			set
			{
				this.assembly_resolver = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001BAD2 File Offset: 0x00019CD2
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001BADA File Offset: 0x00019CDA
		public IMetadataResolver MetadataResolver
		{
			get
			{
				return this.metadata_resolver;
			}
			set
			{
				this.metadata_resolver = value;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001BAE3 File Offset: 0x00019CE3
		public ModuleParameters()
		{
			this.kind = ModuleKind.Dll;
			this.Runtime = ModuleParameters.GetCurrentRuntime();
			this.architecture = TargetArchitecture.I386;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001BB04 File Offset: 0x00019D04
		private static TargetRuntime GetCurrentRuntime()
		{
			return typeof(object).Assembly.ImageRuntimeVersion.ParseRuntime();
		}

		// Token: 0x0400055F RID: 1375
		private ModuleKind kind;

		// Token: 0x04000560 RID: 1376
		private TargetRuntime runtime;

		// Token: 0x04000561 RID: 1377
		private TargetArchitecture architecture;

		// Token: 0x04000562 RID: 1378
		private IAssemblyResolver assembly_resolver;

		// Token: 0x04000563 RID: 1379
		private IMetadataResolver metadata_resolver;
	}
}
