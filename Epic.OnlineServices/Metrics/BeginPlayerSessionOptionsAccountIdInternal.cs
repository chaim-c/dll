using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200030F RID: 783
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct BeginPlayerSessionOptionsAccountIdInternal : IGettable<BeginPlayerSessionOptionsAccountId>, ISettable<BeginPlayerSessionOptionsAccountId>, IDisposable
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public EpicAccountId Epic
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId, MetricsAccountIdType>(this.m_Epic, out result, this.m_AccountIdType, MetricsAccountIdType.Epic);
				return result;
			}
			set
			{
				Helper.Set<MetricsAccountIdType>(value, ref this.m_Epic, MetricsAccountIdType.Epic, ref this.m_AccountIdType, this);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0001F51C File Offset: 0x0001D71C
		public Utf8String External
		{
			get
			{
				Utf8String result;
				Helper.Get<MetricsAccountIdType>(this.m_External, out result, this.m_AccountIdType, MetricsAccountIdType.External);
				return result;
			}
			set
			{
				Helper.Set<MetricsAccountIdType>(value, ref this.m_External, MetricsAccountIdType.External, ref this.m_AccountIdType, this);
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0001F53E File Offset: 0x0001D73E
		public void Set(ref BeginPlayerSessionOptionsAccountId other)
		{
			this.Epic = other.Epic;
			this.External = other.External;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0001F55C File Offset: 0x0001D75C
		public void Set(ref BeginPlayerSessionOptionsAccountId? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.Epic = other.Value.Epic;
				this.External = other.Value.External;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Epic);
			Helper.Dispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0001F5C2 File Offset: 0x0001D7C2
		public void Get(out BeginPlayerSessionOptionsAccountId output)
		{
			output = default(BeginPlayerSessionOptionsAccountId);
			output.Set(ref this);
		}

		// Token: 0x0400096E RID: 2414
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x0400096F RID: 2415
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x04000970 RID: 2416
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
