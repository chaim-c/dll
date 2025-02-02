using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002EF RID: 751
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EnumerateModsCallbackInfoInternal : ICallbackInfoInternal, IGettable<EnumerateModsCallbackInfo>, ISettable<EnumerateModsCallbackInfo>, IDisposable
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0001DF2C File Offset: 0x0001C12C
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0001DF44 File Offset: 0x0001C144
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0001DF50 File Offset: 0x0001C150
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0001DF71 File Offset: 0x0001C171
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

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0001DF84 File Offset: 0x0001C184
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0001DFA5 File Offset: 0x0001C1A5
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
		public ModEnumerationType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0001DFF2 File Offset: 0x0001C1F2
		public void Set(ref EnumerateModsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Type = other.Type;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0001E02C File Offset: 0x0001C22C
		public void Set(ref EnumerateModsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0001E09A File Offset: 0x0001C29A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0001E0B5 File Offset: 0x0001C2B5
		public void Get(out EnumerateModsCallbackInfo output)
		{
			output = default(EnumerateModsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000913 RID: 2323
		private Result m_ResultCode;

		// Token: 0x04000914 RID: 2324
		private IntPtr m_LocalUserId;

		// Token: 0x04000915 RID: 2325
		private IntPtr m_ClientData;

		// Token: 0x04000916 RID: 2326
		private ModEnumerationType m_Type;
	}
}
