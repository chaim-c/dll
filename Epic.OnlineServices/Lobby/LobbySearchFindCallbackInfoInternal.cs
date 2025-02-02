using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039E RID: 926
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindCallbackInfoInternal : ICallbackInfoInternal, IGettable<LobbySearchFindCallbackInfo>, ISettable<LobbySearchFindCallbackInfo>, IDisposable
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0002548C File Offset: 0x0002368C
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x000254A4 File Offset: 0x000236A4
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

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000254B0 File Offset: 0x000236B0
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x000254D1 File Offset: 0x000236D1
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

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000254E4 File Offset: 0x000236E4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000254FC File Offset: 0x000236FC
		public void Set(ref LobbySearchFindCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0002551C File Offset: 0x0002371C
		public void Set(ref LobbySearchFindCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00025560 File Offset: 0x00023760
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0002556F File Offset: 0x0002376F
		public void Get(out LobbySearchFindCallbackInfo output)
		{
			output = default(LobbySearchFindCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B3D RID: 2877
		private Result m_ResultCode;

		// Token: 0x04000B3E RID: 2878
		private IntPtr m_ClientData;
	}
}
