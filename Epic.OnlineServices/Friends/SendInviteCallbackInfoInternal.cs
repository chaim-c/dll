using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000481 RID: 1153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendInviteCallbackInfo>, ISettable<SendInviteCallbackInfo>, IDisposable
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0002B6B0 File Offset: 0x000298B0
		// (set) Token: 0x06001D72 RID: 7538 RVA: 0x0002B6C8 File Offset: 0x000298C8
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

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0002B6D4 File Offset: 0x000298D4
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x0002B6F5 File Offset: 0x000298F5
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

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0002B708 File Offset: 0x00029908
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0002B720 File Offset: 0x00029920
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0002B741 File Offset: 0x00029941
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0002B754 File Offset: 0x00029954
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0002B775 File Offset: 0x00029975
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0002B785 File Offset: 0x00029985
		public void Set(ref SendInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0002B7BC File Offset: 0x000299BC
		public void Set(ref SendInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0002B82A File Offset: 0x00029A2A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0002B851 File Offset: 0x00029A51
		public void Get(out SendInviteCallbackInfo output)
		{
			output = default(SendInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000CFB RID: 3323
		private Result m_ResultCode;

		// Token: 0x04000CFC RID: 3324
		private IntPtr m_ClientData;

		// Token: 0x04000CFD RID: 3325
		private IntPtr m_LocalUserId;

		// Token: 0x04000CFE RID: 3326
		private IntPtr m_TargetUserId;
	}
}
