using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B0 RID: 944
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbyUpdateReceivedCallbackInfo>, ISettable<LobbyUpdateReceivedCallbackInfo>, IDisposable
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0002596C File Offset: 0x00023B6C
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0002598D File Offset: 0x00023B8D
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000259A0 File Offset: 0x00023BA0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x000259B8 File Offset: 0x00023BB8
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x000259D9 File Offset: 0x00023BD9
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

		// Token: 0x060018CC RID: 6348 RVA: 0x000259E9 File Offset: 0x00023BE9
		public void Set(ref LobbyUpdateReceivedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00025A08 File Offset: 0x00023C08
		public void Set(ref LobbyUpdateReceivedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00025A4C File Offset: 0x00023C4C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00025A67 File Offset: 0x00023C67
		public void Get(out LobbyUpdateReceivedCallbackInfo output)
		{
			output = default(LobbyUpdateReceivedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B58 RID: 2904
		private IntPtr m_ClientData;

		// Token: 0x04000B59 RID: 2905
		private IntPtr m_LobbyId;
	}
}
