using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000309 RID: 777
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateModCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateModCallbackInfo>, ISettable<UpdateModCallbackInfo>, IDisposable
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0001EF34 File Offset: 0x0001D134
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x0001EF4C File Offset: 0x0001D14C
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0001EF58 File Offset: 0x0001D158
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x0001EF79 File Offset: 0x0001D179
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

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0001EF8C File Offset: 0x0001D18C
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0001EFAD File Offset: 0x0001D1AD
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0001EFC0 File Offset: 0x0001D1C0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
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

		// Token: 0x060014F5 RID: 5365 RVA: 0x0001F00A File Offset: 0x0001D20A
		public void Set(ref UpdateModCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0001F044 File Offset: 0x0001D244
		public void Set(ref UpdateModCallbackInfo? other)
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

		// Token: 0x060014F7 RID: 5367 RVA: 0x0001F0B2 File Offset: 0x0001D2B2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_Mod);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0001F0D9 File Offset: 0x0001D2D9
		public void Get(out UpdateModCallbackInfo output)
		{
			output = default(UpdateModCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000957 RID: 2391
		private Result m_ResultCode;

		// Token: 0x04000958 RID: 2392
		private IntPtr m_LocalUserId;

		// Token: 0x04000959 RID: 2393
		private IntPtr m_ClientData;

		// Token: 0x0400095A RID: 2394
		private IntPtr m_Mod;
	}
}
