using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000315 RID: 789
	public sealed class MetricsInterface : Handle
	{
		// Token: 0x0600153E RID: 5438 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
		public MetricsInterface()
		{
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0001F8C2 File Offset: 0x0001DAC2
		public MetricsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		public Result BeginPlayerSession(ref BeginPlayerSessionOptions options)
		{
			BeginPlayerSessionOptionsInternal beginPlayerSessionOptionsInternal = default(BeginPlayerSessionOptionsInternal);
			beginPlayerSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_Metrics_BeginPlayerSession(base.InnerHandle, ref beginPlayerSessionOptionsInternal);
			Helper.Dispose<BeginPlayerSessionOptionsInternal>(ref beginPlayerSessionOptionsInternal);
			return result;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0001F90C File Offset: 0x0001DB0C
		public Result EndPlayerSession(ref EndPlayerSessionOptions options)
		{
			EndPlayerSessionOptionsInternal endPlayerSessionOptionsInternal = default(EndPlayerSessionOptionsInternal);
			endPlayerSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_Metrics_EndPlayerSession(base.InnerHandle, ref endPlayerSessionOptionsInternal);
			Helper.Dispose<EndPlayerSessionOptionsInternal>(ref endPlayerSessionOptionsInternal);
			return result;
		}

		// Token: 0x0400097D RID: 2429
		public const int BeginplayersessionApiLatest = 1;

		// Token: 0x0400097E RID: 2430
		public const int EndplayersessionApiLatest = 1;
	}
}
