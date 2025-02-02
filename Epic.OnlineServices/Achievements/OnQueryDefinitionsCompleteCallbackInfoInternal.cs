using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200068B RID: 1675
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryDefinitionsCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryDefinitionsCompleteCallbackInfo>, ISettable<OnQueryDefinitionsCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000405C8 File Offset: 0x0003E7C8
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x000405E0 File Offset: 0x0003E7E0
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

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000405EC File Offset: 0x0003E7EC
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x0004060D File Offset: 0x0003E80D
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

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x00040620 File Offset: 0x0003E820
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x00040638 File Offset: 0x0003E838
		public void Set(ref OnQueryDefinitionsCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x00040658 File Offset: 0x0003E858
		public void Set(ref OnQueryDefinitionsCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0004069C File Offset: 0x0003E89C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000406AB File Offset: 0x0003E8AB
		public void Get(out OnQueryDefinitionsCompleteCallbackInfo output)
		{
			output = default(OnQueryDefinitionsCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04001389 RID: 5001
		private Result m_ResultCode;

		// Token: 0x0400138A RID: 5002
		private IntPtr m_ClientData;
	}
}
