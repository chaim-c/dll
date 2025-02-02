using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000305 RID: 773
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UninstallModCallbackInfoInternal : ICallbackInfoInternal, IGettable<UninstallModCallbackInfo>, ISettable<UninstallModCallbackInfo>, IDisposable
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0001EC14 File Offset: 0x0001CE14
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x0001EC2C File Offset: 0x0001CE2C
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

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0001EC38 File Offset: 0x0001CE38
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x0001EC59 File Offset: 0x0001CE59
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

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0001EC6C File Offset: 0x0001CE6C
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x0001EC8D File Offset: 0x0001CE8D
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

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0001ECD9 File Offset: 0x0001CED9
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

		// Token: 0x060014D5 RID: 5333 RVA: 0x0001ECEA File Offset: 0x0001CEEA
		public void Set(ref UninstallModCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0001ED24 File Offset: 0x0001CF24
		public void Set(ref UninstallModCallbackInfo? other)
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

		// Token: 0x060014D7 RID: 5335 RVA: 0x0001ED92 File Offset: 0x0001CF92
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0001EDB9 File Offset: 0x0001CFB9
		public void Get(out UninstallModCallbackInfo output)
		{
			output = default(UninstallModCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400094A RID: 2378
		private Result m_ResultCode;

		// Token: 0x0400094B RID: 2379
		private IntPtr m_LocalUserId;

		// Token: 0x0400094C RID: 2380
		private IntPtr m_ClientData;

		// Token: 0x0400094D RID: 2381
		private IntPtr m_Mod;
	}
}
