using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200015D RID: 349
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersCallbackInfoInternal : ICallbackInfoInternal, IGettable<UnregisterPlayersCallbackInfo>, ISettable<UnregisterPlayersCallbackInfo>, IDisposable
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0000F000 File Offset: 0x0000D200
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0000F00C File Offset: 0x0000D20C
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0000F02D File Offset: 0x0000D22D
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0000F040 File Offset: 0x0000D240
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000F058 File Offset: 0x0000D258
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0000F07F File Offset: 0x0000D27F
		public ProductUserId[] UnregisteredPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.GetHandle<ProductUserId>(this.m_UnregisteredPlayers, out result, this.m_UnregisteredPlayersCount);
				return result;
			}
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_UnregisteredPlayers, out this.m_UnregisteredPlayersCount);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F095 File Offset: 0x0000D295
		public void Set(ref UnregisterPlayersCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UnregisteredPlayers = other.UnregisteredPlayers;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public void Set(ref UnregisterPlayersCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UnregisteredPlayers = other.Value.UnregisteredPlayers;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F119 File Offset: 0x0000D319
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_UnregisteredPlayers);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F134 File Offset: 0x0000D334
		public void Get(out UnregisterPlayersCallbackInfo output)
		{
			output = default(UnregisterPlayersCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040004A7 RID: 1191
		private Result m_ResultCode;

		// Token: 0x040004A8 RID: 1192
		private IntPtr m_ClientData;

		// Token: 0x040004A9 RID: 1193
		private IntPtr m_UnregisteredPlayers;

		// Token: 0x040004AA RID: 1194
		private uint m_UnregisteredPlayersCount;
	}
}
