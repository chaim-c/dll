using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035D RID: 861
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberCallbackInfoInternal : ICallbackInfoInternal, IGettable<KickMemberCallbackInfo>, ISettable<KickMemberCallbackInfo>, IDisposable
	{
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00021D94 File Offset: 0x0001FF94
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x00021DAC File Offset: 0x0001FFAC
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

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00021DB8 File Offset: 0x0001FFB8
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00021DD9 File Offset: 0x0001FFD9
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

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00021E04 File Offset: 0x00020004
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x00021E25 File Offset: 0x00020025
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

		// Token: 0x060016D4 RID: 5844 RVA: 0x00021E35 File Offset: 0x00020035
		public void Set(ref KickMemberCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00021E60 File Offset: 0x00020060
		public void Set(ref KickMemberCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00021EB9 File Offset: 0x000200B9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00021ED4 File Offset: 0x000200D4
		public void Get(out KickMemberCallbackInfo output)
		{
			output = default(KickMemberCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A57 RID: 2647
		private Result m_ResultCode;

		// Token: 0x04000A58 RID: 2648
		private IntPtr m_ClientData;

		// Token: 0x04000A59 RID: 2649
		private IntPtr m_LobbyId;
	}
}
