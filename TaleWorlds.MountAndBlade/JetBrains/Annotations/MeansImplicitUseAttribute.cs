using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0000F226 File Offset: 0x0000D426
		[UsedImplicitly]
		public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000F230 File Offset: 0x0000D430
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000F246 File Offset: 0x0000D446
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000F250 File Offset: 0x0000D450
		[UsedImplicitly]
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0000F25A File Offset: 0x0000D45A
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0000F262 File Offset: 0x0000D462
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0000F26B File Offset: 0x0000D46B
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0000F273 File Offset: 0x0000D473
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
