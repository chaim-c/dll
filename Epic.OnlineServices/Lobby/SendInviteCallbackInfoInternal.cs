using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003EC RID: 1004
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendInviteCallbackInfo>, ISettable<SendInviteCallbackInfo>, IDisposable
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000267A4 File Offset: 0x000249A4
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x000267BC File Offset: 0x000249BC
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

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000267C8 File Offset: 0x000249C8
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x000267E9 File Offset: 0x000249E9
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000267FC File Offset: 0x000249FC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00026814 File Offset: 0x00024A14
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x00026835 File Offset: 0x00024A35
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

		// Token: 0x06001A0B RID: 6667 RVA: 0x00026845 File Offset: 0x00024A45
		public void Set(ref SendInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00026870 File Offset: 0x00024A70
		public void Set(ref SendInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000268C9 File Offset: 0x00024AC9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000268E4 File Offset: 0x00024AE4
		public void Get(out SendInviteCallbackInfo output)
		{
			output = default(SendInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B91 RID: 2961
		private Result m_ResultCode;

		// Token: 0x04000B92 RID: 2962
		private IntPtr m_ClientData;

		// Token: 0x04000B93 RID: 2963
		private IntPtr m_LobbyId;
	}
}
