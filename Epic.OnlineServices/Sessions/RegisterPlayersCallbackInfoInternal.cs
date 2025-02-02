using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000112 RID: 274
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersCallbackInfoInternal : ICallbackInfoInternal, IGettable<RegisterPlayersCallbackInfo>, ISettable<RegisterPlayersCallbackInfo>, IDisposable
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0000BF9C File Offset: 0x0000A19C
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
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

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
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

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0000C00C File Offset: 0x0000A20C
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0000C033 File Offset: 0x0000A233
		public ProductUserId[] RegisteredPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.GetHandle<ProductUserId>(this.m_RegisteredPlayers, out result, this.m_RegisteredPlayersCount);
				return result;
			}
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_RegisteredPlayers, out this.m_RegisteredPlayersCount);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0000C04C File Offset: 0x0000A24C
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0000C073 File Offset: 0x0000A273
		public ProductUserId[] SanctionedPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.GetHandle<ProductUserId>(this.m_SanctionedPlayers, out result, this.m_SanctionedPlayersCount);
				return result;
			}
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_SanctionedPlayers, out this.m_SanctionedPlayersCount);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000C089 File Offset: 0x0000A289
		public void Set(ref RegisterPlayersCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.RegisteredPlayers = other.RegisteredPlayers;
			this.SanctionedPlayers = other.SanctionedPlayers;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		public void Set(ref RegisterPlayersCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.RegisteredPlayers = other.Value.RegisteredPlayers;
				this.SanctionedPlayers = other.Value.SanctionedPlayers;
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000C12E File Offset: 0x0000A32E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_RegisteredPlayers);
			Helper.Dispose(ref this.m_SanctionedPlayers);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000C155 File Offset: 0x0000A355
		public void Get(out RegisterPlayersCallbackInfo output)
		{
			output = default(RegisterPlayersCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003D0 RID: 976
		private Result m_ResultCode;

		// Token: 0x040003D1 RID: 977
		private IntPtr m_ClientData;

		// Token: 0x040003D2 RID: 978
		private IntPtr m_RegisteredPlayers;

		// Token: 0x040003D3 RID: 979
		private uint m_RegisteredPlayersCount;

		// Token: 0x040003D4 RID: 980
		private IntPtr m_SanctionedPlayers;

		// Token: 0x040003D5 RID: 981
		private uint m_SanctionedPlayersCount;
	}
}
