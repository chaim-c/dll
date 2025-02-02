using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200068F RID: 1679
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryPlayerAchievementsCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryPlayerAchievementsCompleteCallbackInfo>, ISettable<OnQueryPlayerAchievementsCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x00040738 File Offset: 0x0003E938
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x00040750 File Offset: 0x0003E950
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0004075C File Offset: 0x0003E95C
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0004077D File Offset: 0x0003E97D
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x00040790 File Offset: 0x0003E990
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000407A8 File Offset: 0x0003E9A8
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000407C9 File Offset: 0x0003E9C9
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000407D9 File Offset: 0x0003E9D9
		public void Set(ref OnQueryPlayerAchievementsCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00040804 File Offset: 0x0003EA04
		public void Set(ref OnQueryPlayerAchievementsCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0004085D File Offset: 0x0003EA5D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x00040878 File Offset: 0x0003EA78
		public void Get(out OnQueryPlayerAchievementsCompleteCallbackInfo output)
		{
			output = default(OnQueryPlayerAchievementsCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400138E RID: 5006
		private Result m_ResultCode;

		// Token: 0x0400138F RID: 5007
		private IntPtr m_ClientData;

		// Token: 0x04001390 RID: 5008
		private IntPtr m_UserId;
	}
}
