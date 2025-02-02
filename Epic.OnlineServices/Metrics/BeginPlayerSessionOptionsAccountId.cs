using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200030E RID: 782
	public struct BeginPlayerSessionOptionsAccountId
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0001F374 File Offset: 0x0001D574
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0001F38C File Offset: 0x0001D58C
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

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0001F398 File Offset: 0x0001D598
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
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

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0001F400 File Offset: 0x0001D600
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

		// Token: 0x0600151A RID: 5402 RVA: 0x0001F418 File Offset: 0x0001D618
		public static implicit operator BeginPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0001F43C File Offset: 0x0001D63C
		public static implicit operator BeginPlayerSessionOptionsAccountId(Utf8String value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0001F460 File Offset: 0x0001D660
		public static implicit operator BeginPlayerSessionOptionsAccountId(string value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0001F489 File Offset: 0x0001D689
		internal void Set(ref BeginPlayerSessionOptionsAccountIdInternal other)
		{
			this.Epic = other.Epic;
			this.External = other.External;
		}

		// Token: 0x0400096B RID: 2411
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x0400096C RID: 2412
		private EpicAccountId m_Epic;

		// Token: 0x0400096D RID: 2413
		private Utf8String m_External;
	}
}
