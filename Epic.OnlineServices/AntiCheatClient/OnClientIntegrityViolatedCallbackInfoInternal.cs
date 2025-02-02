using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000628 RID: 1576
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientIntegrityViolatedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnClientIntegrityViolatedCallbackInfo>, ISettable<OnClientIntegrityViolatedCallbackInfo>, IDisposable
	{
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x0003C078 File Offset: 0x0003A278
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x0003C099 File Offset: 0x0003A299
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

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0003C0AC File Offset: 0x0003A2AC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		public AntiCheatClientViolationType ViolationType
		{
			get
			{
				return this.m_ViolationType;
			}
			set
			{
				this.m_ViolationType = value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x0003C109 File Offset: 0x0003A309
		public Utf8String ViolationMessage
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ViolationMessage, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ViolationMessage);
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0003C119 File Offset: 0x0003A319
		public void Set(ref OnClientIntegrityViolatedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.ViolationType = other.ViolationType;
			this.ViolationMessage = other.ViolationMessage;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0003C144 File Offset: 0x0003A344
		public void Set(ref OnClientIntegrityViolatedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.ViolationType = other.Value.ViolationType;
				this.ViolationMessage = other.Value.ViolationMessage;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0003C19D File Offset: 0x0003A39D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ViolationMessage);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0003C1B8 File Offset: 0x0003A3B8
		public void Get(out OnClientIntegrityViolatedCallbackInfo output)
		{
			output = default(OnClientIntegrityViolatedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04001228 RID: 4648
		private IntPtr m_ClientData;

		// Token: 0x04001229 RID: 4649
		private AntiCheatClientViolationType m_ViolationType;

		// Token: 0x0400122A RID: 4650
		private IntPtr m_ViolationMessage;
	}
}
