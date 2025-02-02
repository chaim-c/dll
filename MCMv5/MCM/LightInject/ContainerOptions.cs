using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000EB RID: 235
	[ExcludeFromCodeCoverage]
	internal class ContainerOptions
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000DC14 File Offset: 0x0000BE14
		public ContainerOptions()
		{
			this.EnableVariance = true;
			this.EnablePropertyInjection = true;
			this.LogFactory = ((Type t) => delegate(LogEntry message)
			{
			});
			this.EnableCurrentScope = true;
			this.EnableOptionalArguments = false;
			this.OptimizeForLargeObjectGraphs = false;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		public static ContainerOptions Default
		{
			get
			{
				return ContainerOptions.DefaultOptions.Value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000DCCD File Offset: 0x0000BECD
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0000DCD5 File Offset: 0x0000BED5
		public bool EnableVariance { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public Func<Type, bool> VarianceFilter { get; set; } = (Type _) => true;

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		public Func<Type, Action<LogEntry>> LogFactory { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000DD00 File Offset: 0x0000BF00
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000DD08 File Offset: 0x0000BF08
		public Func<string[], string> DefaultServiceSelector { get; set; } = (string[] services) => string.Empty;

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000DD11 File Offset: 0x0000BF11
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000DD19 File Offset: 0x0000BF19
		public bool EnablePropertyInjection { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000DD22 File Offset: 0x0000BF22
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000DD2A File Offset: 0x0000BF2A
		public bool EnableCurrentScope { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000DD33 File Offset: 0x0000BF33
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0000DD3B File Offset: 0x0000BF3B
		public bool EnableOptionalArguments { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000DD44 File Offset: 0x0000BF44
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public bool OptimizeForLargeObjectGraphs { get; set; }

		// Token: 0x06000512 RID: 1298 RVA: 0x0000DD55 File Offset: 0x0000BF55
		private static ContainerOptions CreateDefaultContainerOptions()
		{
			return new ContainerOptions();
		}

		// Token: 0x04000171 RID: 369
		private static readonly Lazy<ContainerOptions> DefaultOptions = new Lazy<ContainerOptions>(new Func<ContainerOptions>(ContainerOptions.CreateDefaultContainerOptions));
	}
}
