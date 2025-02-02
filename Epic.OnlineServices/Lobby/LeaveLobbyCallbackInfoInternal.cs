using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000361 RID: 865
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<LeaveLobbyCallbackInfo>, ISettable<LeaveLobbyCallbackInfo>, IDisposable
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00022080 File Offset: 0x00020280
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00022098 File Offset: 0x00020298
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

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x000220A4 File Offset: 0x000202A4
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x000220C5 File Offset: 0x000202C5
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

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000220D8 File Offset: 0x000202D8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x000220F0 File Offset: 0x000202F0
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00022111 File Offset: 0x00020311
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

		// Token: 0x060016F3 RID: 5875 RVA: 0x00022121 File Offset: 0x00020321
		public void Set(ref LeaveLobbyCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0002214C File Offset: 0x0002034C
		public void Set(ref LeaveLobbyCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000221A5 File Offset: 0x000203A5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000221C0 File Offset: 0x000203C0
		public void Get(out LeaveLobbyCallbackInfo output)
		{
			output = default(LeaveLobbyCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000A64 RID: 2660
		private Result m_ResultCode;

		// Token: 0x04000A65 RID: 2661
		private IntPtr m_ClientData;

		// Token: 0x04000A66 RID: 2662
		private IntPtr m_LobbyId;
	}
}
