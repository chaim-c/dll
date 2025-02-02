using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000359 RID: 857
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinLobbyCallbackInfo>, ISettable<JoinLobbyCallbackInfo>, IDisposable
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00021A64 File Offset: 0x0001FC64
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x00021A7C File Offset: 0x0001FC7C
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00021A88 File Offset: 0x0001FC88
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00021AA9 File Offset: 0x0001FCA9
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

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00021AD4 File Offset: 0x0001FCD4
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x00021AF5 File Offset: 0x0001FCF5
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

		// Token: 0x060016B2 RID: 5810 RVA: 0x00021B05 File Offset: 0x0001FD05
		public void Set(ref JoinLobbyCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00021B30 File Offset: 0x0001FD30
		public void Set(ref JoinLobbyCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00021B89 File Offset: 0x0001FD89
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		public void Get(out JoinLobbyCallbackInfo output)
		{
			output = default(JoinLobbyCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A48 RID: 2632
		private Result m_ResultCode;

		// Token: 0x04000A49 RID: 2633
		private IntPtr m_ClientData;

		// Token: 0x04000A4A RID: 2634
		private IntPtr m_LobbyId;
	}
}
