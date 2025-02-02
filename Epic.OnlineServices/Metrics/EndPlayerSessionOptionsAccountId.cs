using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000312 RID: 786
	public struct EndPlayerSessionOptionsAccountId
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0001F658 File Offset: 0x0001D858
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0001F670 File Offset: 0x0001D870
		public MetricsAccountIdType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			private set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0001F67C File Offset: 0x0001D87C
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
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
				Helper.Set<EpicAccountId, MetricsAccountIdType>(value, ref this.m_Epic, MetricsAccountIdType.Epic, ref this.m_AccountIdType, null);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0001F6BC File Offset: 0x0001D8BC
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public Utf8String External
		{
			get
			{
				Utf8String result;
				Helper.Get<Utf8String, MetricsAccountIdType>(this.m_External, out result, this.m_AccountIdType, MetricsAccountIdType.External);
				return result;
			}
			set
			{
				Helper.Set<Utf8String, MetricsAccountIdType>(value, ref this.m_External, MetricsAccountIdType.External, ref this.m_AccountIdType, null);
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		public static implicit operator EndPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0001F720 File Offset: 0x0001D920
		public static implicit operator EndPlayerSessionOptionsAccountId(Utf8String value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0001F744 File Offset: 0x0001D944
		public static implicit operator EndPlayerSessionOptionsAccountId(string value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0001F76D File Offset: 0x0001D96D
		internal void Set(ref EndPlayerSessionOptionsAccountIdInternal other)
		{
			this.Epic = other.Epic;
			this.External = other.External;
		}

		// Token: 0x04000974 RID: 2420
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04000975 RID: 2421
		private EpicAccountId m_Epic;

		// Token: 0x04000976 RID: 2422
		private Utf8String m_External;
	}
}
