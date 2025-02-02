using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000343 RID: 835
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<DestroyLobbyCallbackInfo>, ISettable<DestroyLobbyCallbackInfo>, IDisposable
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00020C44 File Offset: 0x0001EE44
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00020C5C File Offset: 0x0001EE5C
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

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00020C68 File Offset: 0x0001EE68
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x00020C89 File Offset: 0x0001EE89
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

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00020C9C File Offset: 0x0001EE9C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00020CD5 File Offset: 0x0001EED5
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

		// Token: 0x0600161A RID: 5658 RVA: 0x00020CE5 File Offset: 0x0001EEE5
		public void Set(ref DestroyLobbyCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00020D10 File Offset: 0x0001EF10
		public void Set(ref DestroyLobbyCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00020D69 File Offset: 0x0001EF69
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00020D84 File Offset: 0x0001EF84
		public void Get(out DestroyLobbyCallbackInfo output)
		{
			output = default(DestroyLobbyCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A05 RID: 2565
		private Result m_ResultCode;

		// Token: 0x04000A06 RID: 2566
		private IntPtr m_ClientData;

		// Token: 0x04000A07 RID: 2567
		private IntPtr m_LobbyId;
	}
}
