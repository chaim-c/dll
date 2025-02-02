using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E6 RID: 230
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		[UsedImplicitly]
		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000F1DA File Offset: 0x0000D3DA
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		[UsedImplicitly]
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0000F204 File Offset: 0x0000D404
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0000F20C File Offset: 0x0000D40C
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0000F215 File Offset: 0x0000D415
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0000F21D File Offset: 0x0000D41D
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
