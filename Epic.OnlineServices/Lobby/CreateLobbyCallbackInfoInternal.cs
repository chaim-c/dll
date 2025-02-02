using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033D RID: 829
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<CreateLobbyCallbackInfo>, ISettable<CreateLobbyCallbackInfo>, IDisposable
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00020680 File Offset: 0x0001E880
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00020698 File Offset: 0x0001E898
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

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000206A4 File Offset: 0x0001E8A4
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000206C5 File Offset: 0x0001E8C5
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

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000206F0 File Offset: 0x0001E8F0
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00020711 File Offset: 0x0001E911
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

		// Token: 0x060015DA RID: 5594 RVA: 0x00020721 File Offset: 0x0001E921
		public void Set(ref CreateLobbyCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0002074C File Offset: 0x0001E94C
		public void Set(ref CreateLobbyCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000207A5 File Offset: 0x0001E9A5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000207C0 File Offset: 0x0001E9C0
		public void Get(out CreateLobbyCallbackInfo output)
		{
			output = default(CreateLobbyCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040009E3 RID: 2531
		private Result m_ResultCode;

		// Token: 0x040009E4 RID: 2532
		private IntPtr m_ClientData;

		// Token: 0x040009E5 RID: 2533
		private IntPtr m_LobbyId;
	}
}
