using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200062E RID: 1582
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnMessageToServerCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnMessageToServerCallbackInfo>, ISettable<OnMessageToServerCallbackInfo>, IDisposable
	{
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x0003C224 File Offset: 0x0003A424
		// (set) Token: 0x06002855 RID: 10325 RVA: 0x0003C245 File Offset: 0x0003A445
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

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x0003C258 File Offset: 0x0003A458
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0003C270 File Offset: 0x0003A470
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x0003C297 File Offset: 0x0003A497
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

		// Token: 0x06002859 RID: 10329 RVA: 0x0003C2AD File Offset: 0x0003A4AD
		public void Set(ref OnMessageToServerCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.MessageData = other.MessageData;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0003C2CC File Offset: 0x0003A4CC
		public void Set(ref OnMessageToServerCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.MessageData = other.Value.MessageData;
			}
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0003C310 File Offset: 0x0003A510
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_MessageData);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0003C32B File Offset: 0x0003A52B
		public void Get(out OnMessageToServerCallbackInfo output)
		{
			output = default(OnMessageToServerCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400122D RID: 4653
		private IntPtr m_ClientData;

		// Token: 0x0400122E RID: 4654
		private IntPtr m_MessageData;

		// Token: 0x0400122F RID: 4655
		private uint m_MessageDataSizeBytes;
	}
}
