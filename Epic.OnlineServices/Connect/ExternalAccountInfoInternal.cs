using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200052D RID: 1325
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalAccountInfoInternal : IGettable<ExternalAccountInfo>, ISettable<ExternalAccountInfo>, IDisposable
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x00032C88 File Offset: 0x00030E88
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x00032CA9 File Offset: 0x00030EA9
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductUserId);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00032CBC File Offset: 0x00030EBC
		// (set) Token: 0x060021FF RID: 8703 RVA: 0x00032CDD File Offset: 0x00030EDD
		public Utf8String DisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00032CF0 File Offset: 0x00030EF0
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x00032D11 File Offset: 0x00030F11
		public Utf8String AccountId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00032D24 File Offset: 0x00030F24
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x00032D3C File Offset: 0x00030F3C
		public ExternalAccountType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00032D48 File Offset: 0x00030F48
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x00032D69 File Offset: 0x00030F69
		public DateTimeOffset? LastLoginTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_LastLoginTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LastLoginTime);
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00032D7C File Offset: 0x00030F7C
		public void Set(ref ExternalAccountInfo other)
		{
			this.m_ApiVersion = 1;
			this.ProductUserId = other.ProductUserId;
			this.DisplayName = other.DisplayName;
			this.AccountId = other.AccountId;
			this.AccountIdType = other.AccountIdType;
			this.LastLoginTime = other.LastLoginTime;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00032DD4 File Offset: 0x00030FD4
		public void Set(ref ExternalAccountInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.Value.ProductUserId;
				this.DisplayName = other.Value.DisplayName;
				this.AccountId = other.Value.AccountId;
				this.AccountIdType = other.Value.AccountIdType;
				this.LastLoginTime = other.Value.LastLoginTime;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00032E5E File Offset: 0x0003105E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ProductUserId);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_AccountId);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00032E85 File Offset: 0x00031085
		public void Get(out ExternalAccountInfo output)
		{
			output = default(ExternalAccountInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F1D RID: 3869
		private int m_ApiVersion;

		// Token: 0x04000F1E RID: 3870
		private IntPtr m_ProductUserId;

		// Token: 0x04000F1F RID: 3871
		private IntPtr m_DisplayName;

		// Token: 0x04000F20 RID: 3872
		private IntPtr m_AccountId;

		// Token: 0x04000F21 RID: 3873
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000F22 RID: 3874
		private long m_LastLoginTime;
	}
}
