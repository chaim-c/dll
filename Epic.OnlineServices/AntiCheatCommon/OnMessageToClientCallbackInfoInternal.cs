using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000603 RID: 1539
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnMessageToClientCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnMessageToClientCallbackInfo>, ISettable<OnMessageToClientCallbackInfo>, IDisposable
	{
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x0003AE5C File Offset: 0x0003905C
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x0003AE7D File Offset: 0x0003907D
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

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x0003AE90 File Offset: 0x00039090
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x0003AEA8 File Offset: 0x000390A8
		// (set) Token: 0x06002785 RID: 10117 RVA: 0x0003AEC0 File Offset: 0x000390C0
		public IntPtr ClientHandle
		{
			get
			{
				return this.m_ClientHandle;
			}
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x0003AECC File Offset: 0x000390CC
		// (set) Token: 0x06002787 RID: 10119 RVA: 0x0003AEF3 File Offset: 0x000390F3
		public ArraySegment<byte> MessageData
		{
			get
			{
				ArraySegment<byte> result;
				Helper.Get(this.m_MessageData, out result, this.m_MessageDataSizeBytes);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_MessageData, out this.m_MessageDataSizeBytes);
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0003AF09 File Offset: 0x00039109
		public void Set(ref OnMessageToClientCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.MessageData = other.MessageData;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0003AF34 File Offset: 0x00039134
		public void Set(ref OnMessageToClientCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.MessageData = other.Value.MessageData;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0003AF8D File Offset: 0x0003918D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_MessageData);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0003AFB4 File Offset: 0x000391B4
		public void Get(out OnMessageToClientCallbackInfo output)
		{
			output = default(OnMessageToClientCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040011C2 RID: 4546
		private IntPtr m_ClientData;

		// Token: 0x040011C3 RID: 4547
		private IntPtr m_ClientHandle;

		// Token: 0x040011C4 RID: 4548
		private IntPtr m_MessageData;

		// Token: 0x040011C5 RID: 4549
		private uint m_MessageDataSizeBytes;
	}
}
