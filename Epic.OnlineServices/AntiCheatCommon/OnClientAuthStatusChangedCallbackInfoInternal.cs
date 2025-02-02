using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000601 RID: 1537
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientAuthStatusChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnClientAuthStatusChangedCallbackInfo>, ISettable<OnClientAuthStatusChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0003AC9C File Offset: 0x00038E9C
		// (set) Token: 0x0600276F RID: 10095 RVA: 0x0003ACBD File Offset: 0x00038EBD
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

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0003ACD0 File Offset: 0x00038ED0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x0003ACE8 File Offset: 0x00038EE8
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x0003AD00 File Offset: 0x00038F00
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

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x0003AD0C File Offset: 0x00038F0C
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x0003AD24 File Offset: 0x00038F24
		public AntiCheatCommonClientAuthStatus ClientAuthStatus
		{
			get
			{
				return this.m_ClientAuthStatus;
			}
			set
			{
				this.m_ClientAuthStatus = value;
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0003AD2E File Offset: 0x00038F2E
		public void Set(ref OnClientAuthStatusChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.ClientAuthStatus = other.ClientAuthStatus;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0003AD58 File Offset: 0x00038F58
		public void Set(ref OnClientAuthStatusChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientAuthStatus = other.Value.ClientAuthStatus;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0003ADB1 File Offset: 0x00038FB1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ClientHandle);
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0003ADCC File Offset: 0x00038FCC
		public void Get(out OnClientAuthStatusChangedCallbackInfo output)
		{
			output = default(OnClientAuthStatusChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040011BC RID: 4540
		private IntPtr m_ClientData;

		// Token: 0x040011BD RID: 4541
		private IntPtr m_ClientHandle;

		// Token: 0x040011BE RID: 4542
		private AntiCheatCommonClientAuthStatus m_ClientAuthStatus;
	}
}
