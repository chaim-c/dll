using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F2 RID: 1010
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateLobbyCallbackInfo>, ISettable<UpdateLobbyCallbackInfo>, IDisposable
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00026DF8 File Offset: 0x00024FF8
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00026E10 File Offset: 0x00025010
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

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00026E1C File Offset: 0x0002501C
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00026E3D File Offset: 0x0002503D
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

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00026E50 File Offset: 0x00025050
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00026E68 File Offset: 0x00025068
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00026E89 File Offset: 0x00025089
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

		// Token: 0x06001A49 RID: 6729 RVA: 0x00026E99 File Offset: 0x00025099
		public void Set(ref UpdateLobbyCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00026EC4 File Offset: 0x000250C4
		public void Set(ref UpdateLobbyCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00026F1D File Offset: 0x0002511D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00026F38 File Offset: 0x00025138
		public void Get(out UpdateLobbyCallbackInfo output)
		{
			output = default(UpdateLobbyCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000BAA RID: 2986
		private Result m_ResultCode;

		// Token: 0x04000BAB RID: 2987
		private IntPtr m_ClientData;

		// Token: 0x04000BAC RID: 2988
		private IntPtr m_LobbyId;
	}
}
