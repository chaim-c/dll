using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000355 RID: 853
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyByIdCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinLobbyByIdCallbackInfo>, ISettable<JoinLobbyByIdCallbackInfo>, IDisposable
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00021734 File Offset: 0x0001F934
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x0002174C File Offset: 0x0001F94C
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00021758 File Offset: 0x0001F958
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00021779 File Offset: 0x0001F979
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

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0002178C File Offset: 0x0001F98C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000217A4 File Offset: 0x0001F9A4
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x000217C5 File Offset: 0x0001F9C5
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

		// Token: 0x06001690 RID: 5776 RVA: 0x000217D5 File Offset: 0x0001F9D5
		public void Set(ref JoinLobbyByIdCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00021800 File Offset: 0x0001FA00
		public void Set(ref JoinLobbyByIdCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00021859 File Offset: 0x0001FA59
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00021874 File Offset: 0x0001FA74
		public void Get(out JoinLobbyByIdCallbackInfo output)
		{
			output = default(JoinLobbyByIdCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A39 RID: 2617
		private Result m_ResultCode;

		// Token: 0x04000A3A RID: 2618
		private IntPtr m_ClientData;

		// Token: 0x04000A3B RID: 2619
		private IntPtr m_LobbyId;
	}
}
