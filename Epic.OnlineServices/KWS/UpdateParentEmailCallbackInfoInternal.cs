using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000450 RID: 1104
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParentEmailCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateParentEmailCallbackInfo>, ISettable<UpdateParentEmailCallbackInfo>, IDisposable
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00029E54 File Offset: 0x00028054
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00029E6C File Offset: 0x0002806C
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

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00029E78 File Offset: 0x00028078
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00029E99 File Offset: 0x00028099
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

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00029EAC File Offset: 0x000280AC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00029EC4 File Offset: 0x000280C4
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00029EE5 File Offset: 0x000280E5
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00029EF5 File Offset: 0x000280F5
		public void Set(ref UpdateParentEmailCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00029F20 File Offset: 0x00028120
		public void Set(ref UpdateParentEmailCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00029F79 File Offset: 0x00028179
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00029F94 File Offset: 0x00028194
		public void Get(out UpdateParentEmailCallbackInfo output)
		{
			output = default(UpdateParentEmailCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C88 RID: 3208
		private Result m_ResultCode;

		// Token: 0x04000C89 RID: 3209
		private IntPtr m_ClientData;

		// Token: 0x04000C8A RID: 3210
		private IntPtr m_LocalUserId;
	}
}
