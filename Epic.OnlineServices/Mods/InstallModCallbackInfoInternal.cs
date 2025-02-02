using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F3 RID: 755
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InstallModCallbackInfoInternal : ICallbackInfoInternal, IGettable<InstallModCallbackInfo>, ISettable<InstallModCallbackInfo>, IDisposable
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0001E21C File Offset: 0x0001C41C
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0001E234 File Offset: 0x0001C434
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0001E240 File Offset: 0x0001C440
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0001E261 File Offset: 0x0001C461
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0001E274 File Offset: 0x0001C474
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0001E295 File Offset: 0x0001C495
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0001E2E1 File Offset: 0x0001C4E1
		public ModIdentifier? Mod
		{
			get
			{
				ModIdentifier? result;
				Helper.Get<ModIdentifierInternal, ModIdentifier>(this.m_Mod, out result);
				return result;
			}
			set
			{
				Helper.Set<ModIdentifier, ModIdentifierInternal>(ref value, ref this.m_Mod);
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
		public void Set(ref InstallModCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0001E32C File Offset: 0x0001C52C
		public void Set(ref InstallModCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0001E39A File Offset: 0x0001C59A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0001E3C1 File Offset: 0x0001C5C1
		public void Get(out InstallModCallbackInfo output)
		{
			output = default(InstallModCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000920 RID: 2336
		private Result m_ResultCode;

		// Token: 0x04000921 RID: 2337
		private IntPtr m_LocalUserId;

		// Token: 0x04000922 RID: 2338
		private IntPtr m_ClientData;

		// Token: 0x04000923 RID: 2339
		private IntPtr m_Mod;
	}
}
