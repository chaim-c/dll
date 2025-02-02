using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000313 RID: 787
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct EndPlayerSessionOptionsAccountIdInternal : IGettable<EndPlayerSessionOptionsAccountId>, ISettable<EndPlayerSessionOptionsAccountId>, IDisposable
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0001F78C File Offset: 0x0001D98C
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
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

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0001F800 File Offset: 0x0001DA00
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

		// Token: 0x0600153A RID: 5434 RVA: 0x0001F822 File Offset: 0x0001DA22
		public void Set(ref EndPlayerSessionOptionsAccountId other)
		{
			this.Epic = other.Epic;
			this.External = other.External;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0001F840 File Offset: 0x0001DA40
		public void Set(ref EndPlayerSessionOptionsAccountId? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.Epic = other.Value.Epic;
				this.External = other.Value.External;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0001F884 File Offset: 0x0001DA84
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Epic);
			Helper.Dispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0001F8A6 File Offset: 0x0001DAA6
		public void Get(out EndPlayerSessionOptionsAccountId output)
		{
			output = default(EndPlayerSessionOptionsAccountId);
			output.Set(ref this);
		}

		// Token: 0x04000977 RID: 2423
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04000978 RID: 2424
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x04000979 RID: 2425
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
