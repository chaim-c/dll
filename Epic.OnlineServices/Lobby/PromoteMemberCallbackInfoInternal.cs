using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003DE RID: 990
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberCallbackInfoInternal : ICallbackInfoInternal, IGettable<PromoteMemberCallbackInfo>, ISettable<PromoteMemberCallbackInfo>, IDisposable
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00025CF8 File Offset: 0x00023EF8
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x00025D10 File Offset: 0x00023F10
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00025D1C File Offset: 0x00023F1C
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x00025D3D File Offset: 0x00023F3D
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00025D50 File Offset: 0x00023F50
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00025D68 File Offset: 0x00023F68
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00025D89 File Offset: 0x00023F89
		public Utf8String LobbyId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LobbyId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00025D99 File Offset: 0x00023F99
		public void Set(ref PromoteMemberCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00025DC4 File Offset: 0x00023FC4
		public void Set(ref PromoteMemberCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00025E1D File Offset: 0x0002401D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00025E38 File Offset: 0x00024038
		public void Get(out PromoteMemberCallbackInfo output)
		{
			output = default(PromoteMemberCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B66 RID: 2918
		private Result m_ResultCode;

		// Token: 0x04000B67 RID: 2919
		private IntPtr m_ClientData;

		// Token: 0x04000B68 RID: 2920
		private IntPtr m_LobbyId;
	}
}
